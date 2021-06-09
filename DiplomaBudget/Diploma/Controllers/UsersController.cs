using Diploma.Calculations;
using Diploma.Models.IdentityModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersController()
        {
        }
        public UsersController(ApplicationUserManager userManager, ApplicationRoleManager
        roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                      HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Users
        public ActionResult Index()
        {
            var list = db.Users.ToList();
            return View(list);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.RoleID = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            ViewBag.ManagerID = new SelectList(db.Users.OrderBy(c => c.RealName), "ID", "RealName");
            ViewBag.StructureID = ListCalculate.Structures(db);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminRegisterViewModel userViewModel, params string[] SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.LoginName,
                    Email = userViewModel.LoginName + "@gmail.com",
                    Active = userViewModel.Active,
                    RealName = userViewModel.Name,
                    ParentID = userViewModel.ManagerID,
                    StructureID = userViewModel.StructureID,
                    Position = userViewModel.Position
                };

                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);


                if (adminresult.Succeeded)
                {
                    if (SelectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, SelectedRoles);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleID = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            ViewBag.ManagerID = new SelectList(db.Users.OrderBy(c => c.RealName), "ID", "RealName");
                            ViewBag.StructureID = ListCalculate.Structures(db);
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.ManagerID = new SelectList(db.Users.OrderBy(c => c.RealName), "ID", "RealName");
                    ViewBag.StructureID = ListCalculate.Structures(db);
                    ViewBag.RoleID = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ManagerID = new SelectList(db.Users.OrderBy(c => c.RealName), "ID", "RealName");
            ViewBag.StructureID = ListCalculate.Structures(db);
            ViewBag.RoleID = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var UserRoles = await UserManager.GetRolesAsync(user.Id);
            EditUserViewModel editUser = new EditUserViewModel
            {
                ID = user.Id,                
                Name = user.RealName,
                LoginName = user.UserName,
                Phone = user.PhoneNumber,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = UserRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            };
            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, params string[] SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.ID);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.RealName = editUser.Name;
                user.UserName = editUser.LoginName;
                user.PhoneNumber = editUser.Phone;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                SelectedRoles = SelectedRoles ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, SelectedRoles.Except(userRoles).ToArray());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(SelectedRoles).ToArray());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Произошла ошибка!");
            return View(editUser);
        }
    }
}