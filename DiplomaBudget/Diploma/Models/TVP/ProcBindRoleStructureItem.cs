using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkExtras.EF6;
using System.Data;

namespace Diploma.Models.TVP
{
    //Процедура по изменениею привязки Роль-подразделение
    [StoredProcedure("BindRoleStructureEdit")]
    public class ProcBindRoleStructureEdit
    {
        [StoredProcedureParameter(SqlDbType.Udt,ParameterName ="Test")]
        public List<TVP_StructureList> BindRolesStructure { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar,ParameterName ="Role")]
        public string RoleID { get; set; }

        //Конструкторы
        public ProcBindRoleStructureEdit()
        {
            BindRolesStructure = new List<TVP_StructureList>();
        }

        public ProcBindRoleStructureEdit(string role,int?[] Structures)
        {
            RoleID = role;
            BindRolesStructure = new List<TVP_StructureList>();
            foreach (var item in Structures)
            {
                if (item!=null)
                {
                    TVP_StructureList tvp = new TVP_StructureList(item);
                    BindRolesStructure.Add(tvp);
                }
            }

        }
    }

    //Процедура по изменению привязки Роль-Статья
    [StoredProcedure("BindRoleItemEdit")]
    public class ProcBindRoleItemsEdit
    {
        [StoredProcedureParameter(SqlDbType.Udt,ParameterName ="Test")]
        public List<TVP_BudgetItemsList> BindRolesItems { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, ParameterName = "Role")]
        public string RoleID { get; set; }

        //Конструкторы
        public ProcBindRoleItemsEdit()
        {
            BindRolesItems = new List<TVP_BudgetItemsList>();
        }

        public ProcBindRoleItemsEdit(string role,int?[] BudgetItems)
        {
            RoleID = role;
            BindRolesItems = new List<TVP_BudgetItemsList>();
            foreach (var item in BudgetItems)
            {
                if (item!=null)
                {
                    TVP_BudgetItemsList tvp = new TVP_BudgetItemsList(item);
                    BindRolesItems.Add(tvp);
                }
            }

        }
    }

}