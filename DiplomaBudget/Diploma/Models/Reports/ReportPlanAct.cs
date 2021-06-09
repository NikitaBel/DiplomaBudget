using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.Models.Reports
{
    public class ReportPlanAct
    {
        //Номер строки
        [Display(Name = "Номер строки")]
        public string No { get; set; }

        //Название статьи
        [Display(Name = "Cтатья")]
        public string Name { get; set; }

        //План Мес.        
        [Display(Name = "План Мес. ")]
        public double A { get; set; }

        //Факт Мес.
        [Display(Name = "Факт Мес.")]
        public double B { get; set; }

        //План нарастающий итог
        [Display(Name = "План Нар. Итог")]
        public double C { get; set; }

        //Факт нарастающий итог
        [Display(Name = "Факт Нар. Итог")]
        public double D { get; set; }

        //Отклонение Факт/План
        [Display(Name = "Отклон. Месяц")]
        public double E { get; set; }

        //Отклонение Факт/План нарастающий итог
        [Display(Name = "Отклон. Нар. Итог")]
        public double F { get; set; }
    }
}