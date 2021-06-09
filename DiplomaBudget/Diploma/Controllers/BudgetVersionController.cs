using Diploma.Models;
using Diploma.Models.IdentityModels;
using Diploma.Models.TVP;
using EntityFrameworkExtras.EF6;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    [Authorize(Roles ="Admin,ModelCreater")]
    public class BudgetVersionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BudgetVersion
        public ActionResult Index()
        {
            var list = db.BudgetVersions.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,StartDate,EndDate,Description")] BudgetVersion budgetVersion)
        {
            if (budgetVersion.StartDate>budgetVersion.EndDate)
            {
                ModelState.AddModelError("", "Дата начала должна быть меньше даты конца");
            }
            
            if (ModelState.IsValid)
            {
                budgetVersion.Status = "В работе";
                budgetVersion.ApprovedUserID = User.Identity.GetUserId();
                db.BudgetVersions.Add(budgetVersion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budgetVersion);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetVersion budgetVersion = db.BudgetVersions.Find(id);
            if (budgetVersion == null)
            {
                return HttpNotFound();
            }
            return View(budgetVersion);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetVersion budgetVersion = db.BudgetVersions.Find(id);
            if (budgetVersion == null)
            {
                return HttpNotFound();
            }            
            ViewBag.Status = new SelectList(StatusInitialize());
            ViewBag.ID = id;
            return View(budgetVersion);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Status,StartDate,EndDate,Description")] BudgetVersion budgetVersion)
        {
            if (budgetVersion.StartDate > budgetVersion.EndDate)
            {
                ModelState.AddModelError("", "Дата начала должна быть меньше даты конца");
            }

            if (ModelState.IsValid)
            {
                budgetVersion.ApprovedUserID = User.Identity.GetUserId();
                db.Entry(budgetVersion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(StatusInitialize());
            ViewBag.ID = budgetVersion.ID;
            return View(budgetVersion);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetVersion budgetVersion = db.BudgetVersions.Find(id);
            if (budgetVersion == null)
            {
                return HttpNotFound();
            }
            return View(budgetVersion);
        }
                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetVersion budgetVersion = db.BudgetVersions.Find(id);
            db.BudgetVersions.Remove(budgetVersion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult CreateValueEntry(int Version)
        {
            string UserID = User.Identity.GetUserId();
            ProcCreateValueEntry proc = new ProcCreateValueEntry(Version, UserID);
            db.Database.ExecuteStoredProcedure(proc);
            return new JsonResult { Data = new {} };
        }

        [HttpPost]
        public JsonResult DeleteValueEntry(int Version)
        {
            ProcDeleteValueEntry proc = new ProcDeleteValueEntry(Version);
            db.Database.ExecuteStoredProcedure(proc);
            return new JsonResult { Data = new { } };
        }

        
        public List<string> StatusInitialize()
        {
            var list = new List<string>
            {
                "В работе",
                "Утвержден"
            };
            return list;
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