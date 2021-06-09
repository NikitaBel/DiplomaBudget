using Diploma.Models;
using Diploma.Models.DBViews;
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
    public class BudgetItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BudgetItem
        public ActionResult Index(string Error)
        {
            if (Error == "ErrorDelete") ViewBag.Error = "Для данной статьи присутствуют документы с ненулевой суммой";
            var list = db.BudgetItems.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.BudgetNameID = new SelectList(db.BudgetNames, "ID", "Name");
            ViewBag.ParentID = new SelectList(db.BudgetItems, "ID", "Name");
            ViewBag.ForPlan = ForPlanInitialize();
            ViewBag.ItemType = ItemTypeInitialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BudgetNameID,Active,ParentID,SortOrder,UnaryOperator,MDX_Formula,ForPlan,ItemType")] BudgetItem budget_Item)
        {
            if (ModelState.IsValid)
            {
                if (budget_Item.ParentID != null)
                {
                    budget_Item.Level = db.BudgetItems.Find(budget_Item.ParentID).Level + 1;
                }
                else
                {
                    budget_Item.Level = 0;
                }
                db.BudgetItems.Add(budget_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BudgetNameID = new SelectList(db.BudgetNames, "ID", "Name", budget_Item.BudgetNameID);
            ViewBag.ParentID = new SelectList(db.BudgetItems, "ID", "Name", budget_Item.ParentID);
            return View(budget_Item);
        }
                

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budget_Item = db.BudgetItems.Find(id);
            if (budget_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetNameID = new SelectList(db.BudgetNames, "ID", "Name", budget_Item.BudgetNameID);
            ViewBag.ParentID = new SelectList(db.BudgetItems, "ID", "Name", budget_Item.ParentID);
            ViewBag.ForPlan = ForPlanInitialize();
            ViewBag.ItemType = ItemTypeInitialize();
            return View(budget_Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BudgetNameID,Active,ParentID,SortOrder,UnaryOperator,MDX_Formula,ForPlan,ItemType")] BudgetItem budget_Item)
        {
            if (ModelState.IsValid)
            {
                if (budget_Item.ParentID != null)
                {
                    budget_Item.Level = db.BudgetItems.Find(budget_Item.ParentID).Level + 1;
                }
                else
                {
                    budget_Item.Level = 0;
                }
                db.Entry(budget_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetNameID = new SelectList(db.BudgetNames, "ID", "Name", budget_Item.BudgetNameID);
            ViewBag.ParentID = new SelectList(db.BudgetItems, "ID", "Name", budget_Item.ParentID);
            ViewBag.ForPlan = ForPlanInitialize();
            ViewBag.ItemType = ItemTypeInitialize();
            return View(budget_Item);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budget_Item = db.BudgetItems.Find(id);
            if (budget_Item == null)
            {
                return HttpNotFound();
            }
            List<ValueEntryDetail> details = db.ValueEntryDetails.Where(c => c.ValueEntryHeader.BudgetItem_ID == id).ToList();
            //Проверка заполненности документов с данной статьей
            if (details!=null) {
                double amount = details.Sum(c => c.Amount);
                if (amount != 0) return RedirectToAction("Index",new { Error="ErrorDelete"});
            }

            return View(budget_Item);
        }

        // POST: Budget_Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetItem budget_Item = db.BudgetItems.Find(id);
            db.BudgetItems.Remove(budget_Item);
            db.ValueEntryHeaders.RemoveRange(db.ValueEntryHeaders.Where(c => c.BudgetItem_ID == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public SelectList ForPlanInitialize()
        {
            var list = new[]
            {
                new {Name="Для планирования",Value=0},
                new {Name="Вычисляется",Value=1},
                new {Name="Внешние данные",Value=2}
            };
            return new SelectList(list, "Value", "Name");
        }

        public SelectList ItemTypeInitialize()
        {
            var list = new[]
            {
                new {Name="Доходы",Value=1},
                new {Name="Расходы",Value=-1}
            };
            return new SelectList(list, "Value", "Name");
        }

        public JsonResult LoadMDX()
        {
            string sql = "Select ItemName,ItemNum from [BudgetDB].[dbo].[GetItemMDX]";
            var MDX = db.Database.SqlQuery<ItemMDX>(sql).ToList();
            return new JsonResult { Data = MDX, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}