using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;


namespace Diploma.Models
{
    public class RoleStructure
    {
        public int ID { get; set; }

        public int StructureID { get; set; }

        public string RoleID { get; set; }

        [ForeignKey("StructureID")]
        public virtual Structure Structure { get; set; }

        [ForeignKey("RoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        //Конструкторы 
        public RoleStructure()
        {

        }

        public RoleStructure(int StructID,string appRoleID)
        {
            StructureID = StructID;
            RoleID = appRoleID;
        }
    }
}