using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.Models.Reports
{
    public class ReportDynamic
    {
        [Display(Name ="Статья")]
        public string Name { get; set; }

        [Display(Name = "Янв")]
        public double Jan { get; set; }

        [Display(Name = "Фев")]
        public double Feb { get; set; }
        
        [Display(Name = "Мар")]
        public double Mar { get; set; }

        [Display(Name = "Апр")]
        public double Apr { get; set; }

        [Display(Name = "Май")]
        public double May { get; set; }

        [Display(Name = "Июн")]
        public double Jun { get; set; }

        [Display(Name = "Июл")]
        public double Jul { get; set; }

        [Display(Name = "Авг")]
        public double Aug { get; set; }

        [Display(Name = "Сен")]
        public double Sep { get; set; }

        [Display(Name = "Окт")]
        public double Okt { get; set; }

        [Display(Name = "Ноя")]
        public double Nov { get; set; }

        [Display(Name = "Дек")]
        public double Dec { get; set; }

        [Display(Name = "Итог")]
        public double Total { get; set; }
    }
}