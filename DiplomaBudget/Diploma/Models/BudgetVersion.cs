using Diploma.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class BudgetVersion
    {
        public int ID { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        //Варианты: В работе, Утвержден
        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата От")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата До")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Одобренно")]
        public string ApprovedUserID { get; set; }        

        public virtual List<ValueEntryHeader> ValueEntryHeaders { get; set; }

        public virtual ApplicationUser ApprovedUser { get; set; }
    }
}