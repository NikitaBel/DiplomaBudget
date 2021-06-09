using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    [Table(name: "StructureBudgetItem")]
    public class StructureBudgetItem
    {
        public int ID { get; set; }

        public int StructureID { get; set; }

        public int BudgetItemID { get; set; }

        public virtual Structure Structure { get; set; }

        public virtual BudgetItem BudgetItem { get; set; }

        //Конструкторы
        public StructureBudgetItem()
        {

        }

        public StructureBudgetItem(int StruID, int BudItemID)
        {
            StructureID = StruID;
            BudgetItemID = BudItemID;
        }
    }
}