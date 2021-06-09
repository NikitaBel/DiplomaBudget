using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Models.IdentityModels
{
    public class EditUserViewModel
    {
        public string ID { get; set; }

        [Display(Name = "Фамилия, Имя")]
        public string Name { get; set; }

        [Display(Name = "Логин")]
        public string LoginName { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}