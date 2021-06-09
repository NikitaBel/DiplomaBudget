using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class ValueEntryDetail
    {
        public int ID { get; set; }

        [Display(Name = "Заголовок Документа")]
        public int ValueEntryHeader_ID { get; set; }

        [Display(Name = "Бюджет Дата")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BudgetDateID { get; set; }

        [Display(Name = "Сумма")]
        public double Amount { get; set; }

        [ForeignKey("ValueEntryHeader_ID")]
        public virtual ValueEntryHeader ValueEntryHeader { get; set; }

        [ForeignKey("BudgetDateID")]
        public virtual Dates Dates { get; set; }
    }
}