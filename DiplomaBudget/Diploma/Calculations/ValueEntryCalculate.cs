using Diploma.Models;
using Diploma.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Diploma.Calculations
{
    public class ValueEntryCalculate
    {
        //public static float EntryHeaderAmount(ApplicationDbContext db, ValueEntryHeader valueEntryHeader)
        //{
        //    float Total;
        //    if (db.ValueEntryDetails.Where(b => b.ValueEntryHeader_ID == valueEntryHeader.ID).Count() == 0)
        //    {
        //        Total = 0;
        //    }
        //    else
        //    {
        //        Total = db.ValueEntryDetails.Where(b => b.ValueEntryHeader_ID == valueEntryHeader.ID).Sum(b => b.Amount);
        //    }
        //    return Total;
        //}

        //public static void TotalAmount(ApplicationDbContext db, int id)
        //{
        //    ValueEntryHeader entryHeader = db.ValueEntryHeaders.Find(id);
        //    entryHeader.AmountTotal = EntryHeaderAmount(db, entryHeader);
        //    db.Entry(entryHeader).State = EntityState.Modified;
        //    db.SaveChanges();
        //}
    }
}