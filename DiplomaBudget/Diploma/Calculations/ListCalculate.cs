using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diploma.Models;
using Diploma.Models.IdentityModels;

namespace Diploma.Calculations
{
    public class ListCalculate
    {
        public static SelectList BudgetItems(ApplicationDbContext db)
        {
            var V_item1 = db.BudgetItems.Where(c => c.ParentID != null).Where(c => c.MDX_Formula == null);
            var V_item2 = db.BudgetItems.Where(c => c.ParentID != null);
            var result = from c in V_item1
                         join p in V_item2 on c.ID equals p.ParentID
                         into items
                         from V in items.DefaultIfEmpty()
                         select new { c.ID, c.Name };
            return new SelectList(result, "ID", "Name");
        }

        public static SelectList Structures(ApplicationDbContext db)
        {
            var end_result = db.Structures
                                .Select(t1 => new { ID = t1.ID })
                                .Except(
                                    db.Structures.Where(c => (c.ParentID != null))
                                        .Select(c => new { ID = c.ParentStructure.ID })
                                )
                                .Join(
                                    db.Structures.Where(c => (c.Active == true)),
                                    t0 => t0.ID,
                                    t1 => t1.ID,
                                    (t0, t1) => new { t0 = t0, t1 = t1 }
                                )
                                .Join(db.Structures
                                        .Where(c => (c.ParentID != null)),
                                    temp0 => temp0.t1.ParentID,
                                    t2 => (Int32?)(t2.ID),
                                    (temp0, t2) =>
                                        new
                                        {
                                            t1ID = temp0.t1.ID,
                                            t1Name = temp0.t1.Name,
                                            t2ParentID = t2.ParentID,
                                            t2Name = t2.Name,
                                            Level = t2.Level
                                        }
                                )
                                .Join(
                                    db.Structures,
                                    p1 => p1.t2ParentID,
                                    p2 => (Int32?)(p2.ID),
                                    (p1, p2) =>
                                        new
                                        {
                                            ID = p1.t1ID,
                                            Name = (((p1.Level == 1) ? p1.t2Name : p2.Name) + " " + p1.t1Name)
                                        }
                                );
            return new SelectList(end_result, "ID", "Name");
        }

        public static SelectList StructuresCreate(ApplicationDbContext db)
        {
            var result = db.Structures
                .Where(c => c.Active == true)
                .Select(c => new
                {
                    c.ID
                ,
                    Name = (c.Level == 3 ? c.ParentStructure.ParentStructure.ParentStructure.Name + " | " + c.ParentStructure.ParentStructure.Name + " | " + c.ParentStructure.Name + " | " + c.Name :
                    (c.Level == 2 ? c.ParentStructure.ParentStructure.Name + " | " + c.ParentStructure.Name + " | " + c.Name :
                    (c.Level == 1 ? c.ParentStructure.Name + " | " + c.Name : c.Name)))
                });
            return new SelectList(result, "ID", "Name");
        }

        public static SelectList StructuresEdit(ApplicationDbContext db, int? DefaultValue)
        {
            var result = db.Structures
                .Where(c => c.Active == true)
                .Select(c => new
                {
                    c.ID
                ,
                    Name = (c.Level == 3 ? c.ParentStructure.ParentStructure.ParentStructure.Name + " | " + c.ParentStructure.ParentStructure.Name + " | " + c.ParentStructure.Name + " | " + c.Name :
                    (c.Level == 2 ? c.ParentStructure.ParentStructure.Name + " | " + c.ParentStructure.Name + " | " + c.Name :
                    (c.Level == 1 ? c.ParentStructure.Name + " | " + c.Name : c.Name)))
                });
            return new SelectList(result, "ID", "Name",DefaultValue);
        }
    }
}