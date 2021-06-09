using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Diploma.Models.IdentityModels;

namespace Diploma.Models
{
    public class RoleBudgetItem
    {
        public int ID { get; set; }

        public int BudgetItemID { get; set; }

        public string RoleID { get; set; }

        [ForeignKey("BudgetItemID")]
        public virtual BudgetItem BudgetItem { get; set; }

        [ForeignKey("RoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        //Конструкторы
        public RoleBudgetItem()
        {

        }

        public RoleBudgetItem(string appRoleID,int ItemID)
        {
            BudgetItemID = ItemID;
            RoleID = appRoleID;
        }

    }
}