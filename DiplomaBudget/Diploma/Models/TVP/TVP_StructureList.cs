using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;

namespace Diploma.Models.TVP
{
    [UserDefinedTableType("StructuresList")]
    public class TVP_StructureList
    {
        [UserDefinedTableTypeColumn(1)]
        public int? StructureID { get; set; }

        public TVP_StructureList()
        { }

        public TVP_StructureList(int? Structure)
        {
            StructureID = Structure;
        }
    }
}