using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Diploma.Models.Reports;
using Microsoft.Reporting.WebForms;
using Diploma.Models.IdentityModels;
using Microsoft.AspNet.Identity;


namespace Diploma.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PnL ()
        {
            var Dates = db.Dates.Where(c => c.Date.Year > 2019).Select(c => new {c.StartOfMonth,c.BudgetDate }).Distinct().OrderBy(c=>c.StartOfMonth);
            ViewBag.Version = new SelectList(db.BudgetVersions, "ID", "Name");
            ViewBag.Date = new SelectList(Dates, "StartOfMonth", "BudgetDate");
            return View();
        }

        public ActionResult GetPnl(int Version, DateTime Date)
        {
            var Dates = db.Dates.Where(c => c.Date.Year > 2019).Select(c => new { c.StartOfMonth, c.BudgetDate }).Distinct().OrderBy(c => c.StartOfMonth);
            List<ReportPlanAct> report = new List<ReportPlanAct>();            
            string UserID = User.Identity.GetUserId();
            string sql = "exec spReportPlanAct @BudgetVersion='"+Version+ "', @Month='"+Date.ToString("yyyyddMM")+ "',@UserID='"+ UserID+"'";
            report = db.Database.SqlQuery<ReportPlanAct>(sql).OrderBy(c=>c.No).ToList();
            ViewBag.Version = new SelectList(db.BudgetVersions, "ID", "Name");
            ViewBag.Date = new SelectList(Dates, "StartOfMonth", "BudgetDate");
            return View("Pnl",report);
        }

        public JsonResult GetDates(int Version)
        {
            var StartDate = db.BudgetVersions.Find(Version).StartDate;
            var EndDate = db.BudgetVersions.Find(Version).EndDate;
            var list = db.Dates.Where(c=>c.Date>=StartDate).Where(c=>c.Date<=EndDate).Select(c=>new {c.StartOfMonth,c.BudgetDate }).Distinct().OrderBy(c => c.StartOfMonth).ToList();
            return new JsonResult {Data=list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Dynamics()
        {
            ViewBag.Version = new SelectList(db.BudgetVersions, "ID", "Name");
            ViewBag.EntryType = EntryTypeInit();
            return View();
        }

        public ActionResult GetDynamics(int Version,string EntryType)
        {
            List<ReportDynamic> report = new List<ReportDynamic>();
            string sql = "exec spReportDynamic @BudgetVersion='" + Version + "', @EntryType='" + EntryType + "'";
            report = db.Database.SqlQuery<ReportDynamic>(sql).ToList();
            ViewBag.Version = new SelectList(db.BudgetVersions, "ID", "Name");
            ViewBag.EntryType = EntryTypeInit();
            return View("Dynamics",report);
        }

        public SelectList EntryTypeInit()
        {
            var list = new[]
            {
                new {Name="План"},
                new {Name="Факт"}
            };
            return new SelectList(list, "Name", "Name");
        }

    }
}