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
    public class BudgetNameController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BudgetName
        public ActionResult Index()
        {
            var list = db.BudgetNames.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ForPlan,Active")] BudgetName budgetName)
        {
            if (ModelState.IsValid)
            {
                db.BudgetNames.Add(budgetName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budgetName);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetName budgetName = db.BudgetNames.Find(id);
            if (budgetName == null)
            {
                return HttpNotFound();
            }
            return View(budgetName);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetName budgetName = db.BudgetNames.Find(id);
            if (budgetName == null)
            {
                return HttpNotFound();
            }
            return View(budgetName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ForPlan,Active")] BudgetName budgetName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budgetName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budgetName);
        }
                

        [HttpPost]
        public JsonResult DeleteCheck(int id)
        {
            var list = db.ValueEntryHeaders.Where(c => c.BudgetItem.BudgetNameID == id);

            if (list.Count() >= 1)
            {
                return new JsonResult { Data = new { message = "Есть документы, связанные с данным бюджетом. Для удаления бюджета необходимо предварительно удалить все документы, связанные с этим бюджетом.",id=0 } };
            };
            return new JsonResult { Data = new { message="",id } };
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            BudgetName budgetName = db.BudgetNames.Find(id);
            db.BudgetNames.Remove(budgetName);
            db.SaveChanges();
            return new JsonResult { Data = new { message = "Бюджет был успешно удалён" } };
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