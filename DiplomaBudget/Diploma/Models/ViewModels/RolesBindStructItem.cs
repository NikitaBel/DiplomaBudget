using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Models.ViewModels
{
    public class RolesBindStructItem
    {
        public string ID { get; set; }

        [Required]
        [Display(Name ="Название роли")]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> StructuresList { get; set; }

        public IEnumerable<SelectListItem> BudgetItemsList { get; set; }

    }
}