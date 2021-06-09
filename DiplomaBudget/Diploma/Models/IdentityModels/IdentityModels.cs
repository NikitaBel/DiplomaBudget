using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Diploma.Models.IdentityModels
{
    public class ApplicationUser:IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }

        [Display(Name = "Должность")]
        public string Position { get; set; }        

        public bool Active { get; set; }

        [Display(Name = "Подразделение")]
        public int? StructureID { get; set; }

        [Display(Name = "Руководитель")]
        public string ParentID { get; set; }

        [Display(Name = "Фамилия Имя")]
        public string RealName { get; set; }

        public virtual List<ValueEntryHeader> ValueEntryHeaders { get; set; }

        public virtual List<BudgetVersion> Versions { get; set; }

        public virtual Structure Structure { get; set; }

        public virtual ApplicationUser Parent { get; set; }

    }

    //Этот класс имитирует таблицу [AspNetUserRoles]
    public class UserRoles
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }

    //Перенести этот класс в другую папку (Others - файл GetRealNameByUserID)
    public class IdentityFuctions
    {
        public string GetRealName(string Login, ApplicationDbContext db)
        {
            string Name= db.Users.Where(c => c.UserName == Login).FirstOrDefault().RealName;
            return Name;
        }

        
        public static string GetRealName(string Login)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string Name = db.Users.Where(c => c.UserName == Login).FirstOrDefault().RealName;
            return Name;
        }

    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole():base() { }

        public ApplicationRole(string name):base(name) { }

        public virtual List<RoleStructure> RoleStructures { get; set; }

        public virtual List<RoleBudgetItem> RoleBudgetItems { get; set; }
    }

    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Structure> Structures { get; set; }

        public DbSet<Dates> Dates { get; set; }

        public DbSet<BudgetName> BudgetNames { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<BudgetItem> BudgetItems { get; set; }        

        public DbSet<BudgetVersion> BudgetVersions { get; set; }

        public DbSet<ValueEntryHeader> ValueEntryHeaders { get; set; }

        public DbSet<ValueEntryDetail> ValueEntryDetails { get; set; }


        public DbSet<StructureBudgetItem> StructureBudgetItem { get; set; }

        public DbSet<RoleStructure> RoleStructures { get; set; }

        public DbSet<RoleBudgetItem> RoleBudgetItems { get; set; }
    }
}