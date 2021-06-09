using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;
using System.Data;

namespace Diploma.Models.TVP
{
    [StoredProcedure("BindStructureItemEdit")]
    public class ProcBindStructureBudgetItemEdit
    {
        [StoredProcedureParameter(SqlDbType.Udt,ParameterName ="Test")]
        public List<TVP_BudgetItemsList> BindStructureItems { get; set; }

        [StoredProcedureParameter(SqlDbType.Int,ParameterName ="Structure")]
        public int StructureID { get; set; }

        public ProcBindStructureBudgetItemEdit()
        {
            BindStructureItems = new List<TVP_BudgetItemsList>();
        }

        public ProcBindStructureBudgetItemEdit(int structure, int?[] BudgetItems)
        {
            StructureID = structure;
            BindStructureItems = new List<TVP_BudgetItemsList>();
            foreach(var item in BudgetItems)
            {
                if (item!=null)
                {
                    TVP_BudgetItemsList tvp = new TVP_BudgetItemsList(item);
                    BindStructureItems.Add(tvp);
                }
            }
        }
    }

    [StoredProcedure("BindStructureItemCreate")]
    public class ProcBindStructureBudgetItemCreate
    {
        [StoredProcedureParameter(SqlDbType.Udt, ParameterName = "Test")]
        public List<TVP_BudgetItemsList> BindStructureItems { get; set; }
        

        public ProcBindStructureBudgetItemCreate()
        {
            BindStructureItems = new List<TVP_BudgetItemsList>();
        }

        public ProcBindStructureBudgetItemCreate( int?[] BudgetItems)
        {
            BindStructureItems = new List<TVP_BudgetItemsList>();
            foreach (var item in BudgetItems)
            {
                if (item != null)
                {
                    TVP_BudgetItemsList tvp = new TVP_BudgetItemsList(item);
                    BindStructureItems.Add(tvp);
                }
            }
        }
    }
}