using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;
using System.Data;

namespace Diploma.Models.TVP
{
    [StoredProcedure("DeleteValueEntry")]
    public class ProcDeleteValueEntry
    {
        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "Version")]
        public int VersionID { get; set; }

        public ProcDeleteValueEntry()
        {

        }

        public ProcDeleteValueEntry(int Version)
        {
            VersionID = Version;
        }
    }
}