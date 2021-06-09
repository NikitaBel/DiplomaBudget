using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class Structure
    {
        public int ID { get; set; }

        [Display(Name = "Подразделение Имя")]
        public string Name { get; set; }

        [Display(Name = "Активен")]
        public bool Active { get; set; }

        [Display(Name = "Подчинение")]
        public int? ParentID { get; set; }

        [Display(Name = "Уровень")]
        public int Level { get; set; }

        [ForeignKey("ParentID")]
        public virtual Structure ParentStructure { get; set; }

        public virtual ICollection<StructureBudgetItem> Structure_Budget_Items { get; set; }

        public virtual List<ValueEntryHeader> ValueEntryHeaders { get; set; }

    }
}