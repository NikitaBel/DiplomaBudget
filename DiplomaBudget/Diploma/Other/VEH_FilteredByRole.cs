using Diploma.Models;
using Diploma.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Diploma.Other
{
    public class VEH_FilteredByRole
    {
        public static List<ValueEntryHeader> GetHeaders(string UserID,ApplicationDbContext db)
        {
            List<ValueEntryHeader> list = new List<ValueEntryHeader>();
            string sql = "SELECT  * FROM [BudgetDB].[dbo].[AspNetUserRoles] where UserID= '" + UserID+"'";
            var Roles = db.Database.SqlQuery<UserRoles>(sql).ToList();

            var result =
                from t1 in Roles
                join t2 in db.RoleStructures on t1.RoleId equals t2.RoleID
                join t3 in db.RoleBudgetItems on t1.RoleId equals t3.RoleID
                join t4 in db.ValueEntryHeaders on new { Structure_ID = t2.StructureID, BudgetItem_ID = t3.BudgetItemID } equals new { t4.Structure_ID, t4.BudgetItem_ID }
                select t4;

            list = result.ToList();
            return list;
        }

    }
}