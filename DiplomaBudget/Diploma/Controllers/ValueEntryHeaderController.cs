using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diploma.Calculations;
using Diploma.Models;
using Diploma.Models.IdentityModels;
using Diploma.Models.TVP;
using Diploma.Models.ViewModels;
using Diploma.Other;
using Microsoft.AspNet.Identity;
using EntityFrameworkExtras.EF6;
using System.Data.SqlClient;

namespace Diploma.Controllers
{
    [Authorize]
    public class ValueEntryHeaderController : Controller
    {
        //Конструкторы для класса контролера
        public ValueEntryHeaderController() { }

        public ValueEntryHeaderController(VEH_Filters filters)
        {
            Filters = filters;
        }
        
        //Класс контекста для доступа к объектам БД
        private ApplicationDbContext db = new ApplicationDbContext();
        //Класс фильтра для сохранения фильтров при переходе между страницами
        //Класс статический, потому что при вызове действия КОНТРОЛЛЕР ЗАНОВО ИНИЦИАЛИЗИРУЕТСЯ
        public static VEH_Filters Filters = new VEH_Filters();

        // GET: ValueEntryHeader
        public ActionResult Index()
        {
            List<ValueEntryHeader> list = new List<ValueEntryHeader>();
            list = VEH_FilteredByRole.GetHeaders(User.Identity.GetUserId(),db);

            if (Filters.GetVersion() != null)
                list = list.Where(d => d.Version_ID == Filters.GetVersion()).Where(c => c.EntryType == "План").ToList();
            else list = list.Where(c => c.EntryType == "План").Where(c => c.Version.Status == "В работе").ToList();

            if (Filters.GetStructure() != null)
                list = list.Where(c => c.Structure_ID == Filters.GetStructure()).ToList();

            if (Filters.GetItem() != null)
                list = list.Where(c => c.BudgetItem_ID == Filters.GetItem()).ToList();

            ViewBag.Item = new SelectList(db.BudgetItems, "ID", "Name");
            ViewBag.Structure = new SelectList(db.Structures, "ID", "Name");
            ViewBag.Version = new SelectList(db.BudgetVersions.Where(c=>c.Status=="В работе"),"ID","Name");
            return View(list.ToList());            
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValueEntryHeader header = db.ValueEntryHeaders.Find(id);
            if (header==null)
            {
                return HttpNotFound();
            }
            String Sum = header.ValueEntryDetails.Sum(c => c.Amount).ToString();
            ViewBag.AmountTotal = Sum;                        
            return View(header);
        }

        [HttpPost]        
        public JsonResult Edit(List<ValueEntryDetail> details)
        {
            bool status = true;            
            try
            {
                foreach (var item in details)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();                
            }
            catch
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
        
        //Смена статуса документа (параметры кроме id нужны для фильтров)
        public ActionResult ChangeWorking(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValueEntryHeader header = db.ValueEntryHeaders.Find(id);
            if (header==null)
            {
                return HttpNotFound();
            }
            header.IsWorking = !header.IsWorking;
            db.Entry(header).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Сохранение фильтров 
        public ActionResult SaveFilter(int? Version, int? Item, int? Structure)
        {
            Filters.FillFilters(Version, Structure, Item);
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            ViewBag.Version = new SelectList(db.BudgetVersions.Where(c => c.Status == "В работе"), "ID", "Name");
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            List<ReportPlanVEH> reports = new List<ReportPlanVEH>();
            return View(reports);
        }

        //[HttpPost,ActionName("Report")]        
        public ActionResult GetReport(int Version,int[] BudgetNames)
        {
            List<ReportPlanVEH> reports = new List<ReportPlanVEH>();
            string list = BudgetNames[0].ToString();
            for (int i=1;i<BudgetNames.Length;i++)
            {
                list += ","+BudgetNames[i];
            }
            
            string sql = "exec spIsWorkingVEH @VersionID='"+ Version.ToString()+ "', @BudgetNames='"+list+"'";
            reports = db.Database.SqlQuery<ReportPlanVEH>(sql).ToList();
            ViewBag.Version = new SelectList(db.BudgetVersions.Where(c => c.Status == "В работе"), "ID", "Name");
            ViewBag.BudgetName = new SelectList(db.BudgetNames, "ID", "Name");
            return View("Report", reports);
        }

    }
}