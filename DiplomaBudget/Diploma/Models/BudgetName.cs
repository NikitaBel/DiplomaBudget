using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class BudgetName
    {
        public int ID { get; set; }

        [Display(Name = "Имя Бюджета")]
        public string Name { get; set; }

        [Display(Name = "Активен")]
        public bool Active { get; set; }

        public virtual List<BudgetItem> Budget_Items { get; set; }
    }
}