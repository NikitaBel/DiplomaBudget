using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diploma.Models.IdentityModels;
using System.Threading.Tasks;
using System.Net;
using Diploma.Models.ViewModels;
using Diploma.Models;
using Diploma.Models.TVP;
using EntityFrameworkExtras.EF6;

namespace Diploma.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public RolesController() { }

        public RolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager Umanager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return Umanager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                Umanager = value;
            }
        }

        private ApplicationRoleManager Rmanager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return Rmanager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                Rmanager = value;
            }
        }


        // GET: Roles
        public ActionResult Index()
        {
            var list = RoleManager.Roles.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.Structure = new SelectList(db.Structures, "ID", "Name");
            ViewBag.BudgetItem = new SelectList(db.BudgetItems.Where(c => c.ForPlan == 0), "ID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RolesBindStructItem rolesBindModel, int[] SelectedStructers, int[] SelectedItems)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole(rolesBindModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    ViewBag.Structure = new SelectList(db.Structures, "ID", "Name");
                    ViewBag.BudgetItem = new SelectList(db.BudgetItems.Where(c => c.ForPlan == 0), "ID", "Name");
                    return View();
                }
                if (SelectedStructers != null)
                {
                    foreach (int item in SelectedStructers)
                    {
                        RoleStructure roleStructure = new RoleStructure(item, role.Id);
                        db.RoleStructures.Add(roleStructure);
                    }
                }
                if (SelectedStructers != null)
                {
                    foreach (int item in SelectedItems)
                    {
                        RoleBudgetItem roleBudget = new RoleBudgetItem(role.Id, item);
                        db.RoleBudgetItems.Add(roleBudget);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Structure = new SelectList(db.Structures, "ID", "Name");
            ViewBag.BudgetItem = new SelectList(db.BudgetItems.Where(c => c.ForPlan == 0), "ID", "Name");
            
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole role = await RoleManager.FindByIdAsync(id);

            var StructureID = db.RoleStructures.Where(c => c.RoleID == id).ToList().Select(c => c.StructureID);
            var ItemID = db.RoleBudgetItems.Where(c => c.RoleID == id).Where(c => c.BudgetItem.ForPlan == 0).ToList().Select(c => c.BudgetItemID);

            RolesBindStructItem model = new RolesBindStructItem
            {
                ID = role.Id,
                Name = role.Name,
                BudgetItemsList = db.BudgetItems.Where(c => c.ForPlan == 0).ToList().Select(x => new SelectListItem()
                {
                    Selected = ItemID.Contains(x.ID),
                    Text = x.Name,
                    Value = x.ID.ToString()
                }),
                StructuresList = db.Structures.ToList().Select(x=>new SelectListItem()
                {
                    Selected = StructureID.Contains(x.ID),
                    Text = x.Name,
                    Value = x.ID.ToString()
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RolesBindStructItem rolesBindModel, int?[] SelectedStructures, int?[] SelectedItems)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(rolesBindModel.ID);
                role.Name = rolesBindModel.Name;
                var roleresult = await RoleManager.UpdateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View(rolesBindModel);
                }
                //Проверка на наличие выбранных подразделений
                if (SelectedStructures!=null)
                {
                    ProcBindRoleStructureEdit procedureStructure = new ProcBindRoleStructureEdit(role.Id, SelectedStructures);
                    db.Database.ExecuteStoredProcedure(procedureStructure);
                }
                //Проверка на наличие выбранных статей
                if (SelectedItems!=null)
                {
                    ProcBindRoleItemsEdit procedureItems = new ProcBindRoleItemsEdit(role.Id, SelectedItems);
                    db.Database.ExecuteStoredProcedure(procedureItems);
                }
                return RedirectToAction("Index");
            }
            return View(rolesBindModel);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            string[] membersIDs = role.Users.Select(c => c.UserId).ToArray();
            List<ApplicationUser> members = UserManager.Users.Where(c => membersIDs.Any(y => y == c.Id)).ToList();
            RolesDetailViewModel viewModel = new RolesDetailViewModel(role, members);
            return View(viewModel);
        }

    }
}