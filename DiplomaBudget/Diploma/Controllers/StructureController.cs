using Diploma.Calculations;
using Diploma.Models;
using Diploma.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    [Authorize]
    public class StructureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Structure
        public ActionResult Index(string Error)
        {
            if (Error == "ErrorDelete") ViewBag.Error = "Для данного подразделения присутствуют документы с ненулевой суммой";
            var list = db.Structures.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.ParentID = ListCalculate.StructuresCreate(db);
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            ViewBag.BudgetItem = new SelectList(db.BudgetItems, "ID", "Name");
            return View();
        }

        
        [HttpPost]
        public JsonResult CreateSave([Bind(Include = "ID,Name,ParentID,Active,Structure_Budget_Items")] Structure structure)
        {
            bool status = false;
            var isValidModel = TryUpdateModel(structure);
            if (isValidModel)
            {
                if (structure.ParentID != null)
                {
                    structure.Level = db.Structures.Find(structure.ParentID).Level + 1;
                }
                else
                {
                    structure.Level = 0;
                }
                db.Structures.Add(structure);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Structure structure = db.Structures.Find(id);
            if (structure == null)
            {
                return HttpNotFound();
            }            
            SelectList list = ListCalculate.StructuresEdit(db,structure.ParentID);            
            ViewBag.ParentID = list;            
            return View(structure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ParentID,Active")] Structure structure)
        {
            if (ModelState.IsValid)
            {
                if (structure.ParentID != null)
                {
                    structure.Level = db.Structures.Find(structure.ParentID).Level + 1;
                }
                else
                {
                    structure.Level = 0;
                }
                db.Entry(structure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SelectList list = ListCalculate.StructuresEdit(db, structure.ParentID);
            ViewBag.ParentID = list;            
            return View(structure);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Structure structure = db.Structures.Find(id);
            if (structure == null)
            {
                return HttpNotFound();
            }
            List<ValueEntryDetail> details = db.ValueEntryDetails.Where(c => c.ValueEntryHeader.Structure_ID == id).ToList();
            //Проверка заполненности документов с данной статьей
            if (details != null)
            {
                double amount = details.Sum(c => c.Amount);
                if (amount != 0) return RedirectToAction("Index", new { Error = "ErrorDelete" });
            }

            return View(structure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete ( Structure structure)
        {
            //Удаление ЦФО происходит по этой переменной, потому что из формы поступает только ID ЦФО
            Structure Delstructure = db.Structures.Find(structure.ID);
            var list = db.StructureBudgetItem.Where(c => c.StructureID == structure.ID).ToList();
            db.StructureBudgetItem.RemoveRange(list);
            db.Structures.Remove(Delstructure);
            db.ValueEntryHeaders.RemoveRange(db.ValueEntryHeaders.Where(c => c.Structure_ID == structure.ID));
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult CreateBudgetItemBind(int StructureID)
        {
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            ViewBag.BudgetItemID = new SelectList(db.BudgetItems.Where(c=>c.ForPlan==0), "ID", "Name");
            ViewBag.StructureID = StructureID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudgetItemBind([Bind(Include = "ID,StructureID,BudgetItemID")]  StructureBudgetItem structureBudgetItem)
        {
            if (ModelState.IsValid)
            {
                //Проверка существует ли уже привязки с данной статьей
                if (db.StructureBudgetItem.Where(c=>c.BudgetItemID== structureBudgetItem.BudgetItemID && c.StructureID== structureBudgetItem.StructureID).Count()>0)
                {
                    ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
                    ViewBag.BudgetItemID = new SelectList(db.BudgetItems, "ID", "Name");
                    ViewBag.StructureID = structureBudgetItem.StructureID;
                    ModelState.AddModelError("", "Привязка к этой статье уже существует.");                    
                    return View();
                }

                db.StructureBudgetItem.Add(structureBudgetItem);
                db.SaveChanges();
                return RedirectToAction("Edit",new { id= structureBudgetItem.StructureID });
            }
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            ViewBag.BudgetItemID = new SelectList(db.BudgetItems, "ID", "Name");
            ViewBag.StructureID = structureBudgetItem.StructureID;
            return View();
        }

        public ActionResult EditBudgetItemBind(int StructureID, int BudgetItemID)
        {
            var item = db.StructureBudgetItem.Where(c => c.StructureID == StructureID && c.BudgetItemID == BudgetItemID).FirstOrDefault();            
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            ViewBag.BudgetItemID = new SelectList(db.BudgetItems.Where(c => c.ForPlan == 0), "ID", "Name");
            ViewBag.StructureID = StructureID;
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBudgetItemBind([Bind(Include = "ID,StructureID,BudgetItemID")] StructureBudgetItem structureBudgetItem)
        {
            if (ModelState.IsValid)
            {
                //Проверка существует ли уже привязки с данной статьей
                if (db.StructureBudgetItem.Where(c => c.BudgetItemID == structureBudgetItem.BudgetItemID && c.StructureID == structureBudgetItem.StructureID).Count() > 0)
                {
                    ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
                    ViewBag.BudgetItemID = new SelectList(db.BudgetItems, "ID", "Name");
                    ViewBag.StructureID = structureBudgetItem.StructureID;
                    ModelState.AddModelError("", "Уже была произведена привязка статьи к данному документу.");
                    return View(structureBudgetItem);
                }

                db.Entry(structureBudgetItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = structureBudgetItem.StructureID });
            }
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            ViewBag.BudgetItemID = new SelectList(db.BudgetItems, "ID", "Name");
            ViewBag.StructureID = structureBudgetItem.StructureID;
            return View(structureBudgetItem);
        }

        [HttpPost]
        public JsonResult DelBudgetItemBind(int id)
        {
            //bool status = false;
            StructureBudgetItem structureBudget = db.StructureBudgetItem.Find(id);
            db.StructureBudgetItem.Remove(structureBudget);
            db.SaveChanges();
            return new JsonResult { Data = new { } };
        }


        public JsonResult LoadItems(int BudgetName)
        {
            var list = db.BudgetItems.Where(c => c.BudgetNameID == BudgetName).Where(c => c.ForPlan == 0).Select(c => new { c.ID, c.Name });
            return new JsonResult {Data=list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}