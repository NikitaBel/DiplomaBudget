using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;
using System.Data;


namespace Diploma.Models.TVP
{
    [StoredProcedure("spUpdateCostPlanVED")]
    public class ProcUpdateCostsPlan
    {
        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "VersionID")]
        public int VersionID { get; set; }

        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "SaleID")]
        public int ItemID { get; set; }

        public ProcUpdateCostsPlan() { }

        public ProcUpdateCostsPlan(int Version, int Item)
        {
            VersionID = Version;
            ItemID = Item;
        }
    }
}