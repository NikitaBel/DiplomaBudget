using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class Dates
    {
        [Key]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }

        public int YearName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime YearDate { get; set; }

        public string MMMM { get; set; }

        public string MMM { get; set; }

        public int M { get; set; }

        public string BudgetDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartOfMonth { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndOfMonth { get; set; }

        public int QuarterNo { get; set; }

        public string QuarterName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartOfQrt { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndOfQrt { get; set; }

        public virtual List<ValueEntryHeader> ValueEntryHeaders { get; set; }

        public virtual List<ValueEntryDetail> ValueEntryDeatils { get; set; }
    }
}