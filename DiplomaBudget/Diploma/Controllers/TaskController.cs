using Diploma.Models.IdentityModels;
using Diploma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diploma.Models.TVP;
using EntityFrameworkExtras.EF6;


namespace Diploma.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadItems()
        {
            var list = db.BudgetItems.Where(c => c.ForPlan == 0).Select(c=>new {c.ID,c.Name });
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult LoadVersions()
        {
            var list = db.BudgetVersions.Where(c => c.Status == "В работе").Select(c=>new {c.ID,c.Name });
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UpdateSalesPlan(int Version,int Item)
        {
            ProcUpdateSalesPlan proc = new ProcUpdateSalesPlan(Version, Item);
            db.Database.ExecuteStoredProcedure(proc);
            return new JsonResult { Data = new { } };
        }

        [HttpPost]
        public JsonResult UpdateCostsPlan(int Version, int Item)
        {
            ProcUpdateCostsPlan proc = new ProcUpdateCostsPlan(Version, Item);
            db.Database.ExecuteStoredProcedure(proc);
            return new JsonResult { Data = new { } };
        }
    }
}