using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;
using System.Data;

namespace Diploma.Models.TVP
{
    [StoredProcedure("CreateValueEntry")]
    public class ProcCreateValueEntry
    {
        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "Version")]
        public int VersionID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, ParameterName = "UserID")]
        public string UserID { get; set; }

        public ProcCreateValueEntry()
        {

        }

        public ProcCreateValueEntry(int Version,string User)
        {
            VersionID = Version;
            UserID = User;
        }
    }
}