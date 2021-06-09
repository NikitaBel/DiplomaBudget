using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Models.ViewModels
{
    public class ReportPlanVEH
    {
        public string BudgetName { get; set; }
        public string ItemName { get; set; }
        public string StructureName { get; set; }
        public bool IsWorking { get; set; }
    }

    public class ReportPlanVEH_View
    {
        public string BudgetName { get; set; }

        public List<ReportPlanVEH_Structure> Structures { get; set; }

    }

    public class ReportPlanVEH_Structure
    {
        public string StructureName { get; set; }

        public List<ReportPlanVEH_Item> Structures { get; set; }
    }

    public class ReportPlanVEH_Item
    {
        public string ItemName { get; set; }

        public bool IsWorking { get; set; }
    }


}