using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;

namespace Diploma.Models.TVP
{
    [UserDefinedTableType("BudgetItemsList")]
    public class TVP_BudgetItemsList
    {
        [UserDefinedTableTypeColumn(1)]
        public int? BudgetItemID { get; set; }

        public TVP_BudgetItemsList()
        { }

        public TVP_BudgetItemsList(int? BudgetItem)
        {
            BudgetItemID = BudgetItem;
        }
    }
}