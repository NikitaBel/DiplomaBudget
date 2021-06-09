using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class BudgetItem
    {
        public int ID { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Название бюджета")]
        public int BudgetNameID { get; set; }

        [Display(Name = "Активен")]
        public bool Active { get; set; }

        [Display(Name = "Родитель")]
        public int? ParentID { get; set; }

        [Display(Name = "Действие")]
        public string UnaryOperator { get; set; }

        [Display(Name = "Расчёт")]
        public string MDX_Formula { get; set; }

        [Display(Name = "Уровень")]
        public int Level { get; set; }

        [Display(Name = "Порядок Сортировки")]
        public Int16 SortOrder { get; set; }

        [Display(Name = "Для планирования")]
        //0 - Для планирования, 1 - Вычисляется, 2 -Внешние данные
        public Int16 ForPlan { get; set; }

        [Display(Name = "Тип Статьи")]
        //-1 - расходы, 1 - доходы
        public Int16 ItemType { get; set; }

        public virtual BudgetName BudgetName { get; set; }

        [ForeignKey("ParentID")]
        public virtual BudgetItem ParentBudget_Item { get; set; }

        public virtual ICollection<StructureBudgetItem> StructureBudgetItem { get; set; }

        public virtual List<ValueEntryHeader> ValueEntryHeaders { get; set; }

    }
}