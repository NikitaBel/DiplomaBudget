namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BindRoleStructureItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RolesRFPUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RolesRFPUsers", "RoleRFPID", "dbo.RolesRFPs");
            DropForeignKey("dbo.RequirementForPayments", "Structure_ID1", "dbo.Structures");
            DropForeignKey("dbo.RequirementForPayments", "Applicant_ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequirementForPayments", "Budget_Item_ID", "dbo.BudgetItems");
            DropForeignKey("dbo.RequirementForPayments", "Contractor_ID1", "dbo.Contractors");
            DropForeignKey("dbo.ListOfPayments", "RequirementForPayment_ID1", "dbo.RequirementForPayments");
            DropForeignKey("dbo.RequirementForPayments", "Resolution_0", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequirementForPayments", "Resolution_1", "dbo.AspNetUsers");
            DropIndex("dbo.RequirementForPayments", new[] { "Applicant_ID" });
            DropIndex("dbo.RequirementForPayments", new[] { "Resolution_0" });
            DropIndex("dbo.RequirementForPayments", new[] { "Resolution_1" });
            DropIndex("dbo.RequirementForPayments", new[] { "Structure_ID1" });
            DropIndex("dbo.RequirementForPayments", new[] { "Budget_Item_ID" });
            DropIndex("dbo.RequirementForPayments", new[] { "Contractor_ID1" });
            DropIndex("dbo.RolesRFPUsers", new[] { "UserID" });
            DropIndex("dbo.RolesRFPUsers", new[] { "RoleRFPID" });
            DropIndex("dbo.ListOfPayments", new[] { "RequirementForPayment_ID1" });
            CreateTable(
                "dbo.RoleBudgetItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BudgetItemID = c.Int(nullable: false),
                        RoleID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleID)
                .ForeignKey("dbo.BudgetItems", t => t.BudgetItemID, cascadeDelete: true)
                .Index(t => t.BudgetItemID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.RoleStructures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StructureID = c.Int(nullable: false),
                        RoleID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleID)
                .ForeignKey("dbo.Structures", t => t.StructureID, cascadeDelete: true)
                .Index(t => t.StructureID)
                .Index(t => t.RoleID);
            
            DropTable("dbo.RequirementForPayments");
            DropTable("dbo.RolesRFPUsers");
            DropTable("dbo.RolesRFPs");
            DropTable("dbo.Contractors");
            DropTable("dbo.ListOfPayments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ListOfPayments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequirementForPayment_ID = c.Int(nullable: false),
                        Date_of_Payment = c.DateTime(nullable: false),
                        Payment_Document = c.Int(nullable: false),
                        Payed_Amount = c.Single(nullable: false),
                        RequirementForPayment_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VAT_Registration_No = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RolesRFPs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RolesRFPUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        RoleRFPID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RequirementForPayments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Structure_ID = c.Int(nullable: false),
                        BudgetItem_ID = c.Int(nullable: false),
                        Contractor_ID = c.Int(nullable: false),
                        BudgetDate = c.DateTime(nullable: false),
                        Pay_Before_Date = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Status = c.String(),
                        Description = c.String(),
                        Applicant_ID = c.String(maxLength: 128),
                        Resolution_0 = c.String(maxLength: 128),
                        Resolution_1 = c.String(maxLength: 128),
                        Required_Amount = c.Single(nullable: false),
                        Budgeted_Amount = c.Single(nullable: false),
                        Approved_Amount = c.Single(nullable: false),
                        Fact_Payed_Amount = c.Single(nullable: false),
                        Structure_ID1 = c.Int(),
                        Budget_Item_ID = c.Int(),
                        Contractor_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.RoleStructures", "StructureID", "dbo.Structures");
            DropForeignKey("dbo.RoleStructures", "RoleID", "dbo.AspNetRoles");
            DropForeignKey("dbo.RoleBudgetItems", "BudgetItemID", "dbo.BudgetItems");
            DropForeignKey("dbo.RoleBudgetItems", "RoleID", "dbo.AspNetRoles");
            DropIndex("dbo.RoleStructures", new[] { "RoleID" });
            DropIndex("dbo.RoleStructures", new[] { "StructureID" });
            DropIndex("dbo.RoleBudgetItems", new[] { "RoleID" });
            DropIndex("dbo.RoleBudgetItems", new[] { "BudgetItemID" });
            DropTable("dbo.RoleStructures");
            DropTable("dbo.RoleBudgetItems");
            CreateIndex("dbo.ListOfPayments", "RequirementForPayment_ID1");
            CreateIndex("dbo.RolesRFPUsers", "RoleRFPID");
            CreateIndex("dbo.RolesRFPUsers", "UserID");
            CreateIndex("dbo.RequirementForPayments", "Contractor_ID1");
            CreateIndex("dbo.RequirementForPayments", "Budget_Item_ID");
            CreateIndex("dbo.RequirementForPayments", "Structure_ID1");
            CreateIndex("dbo.RequirementForPayments", "Resolution_1");
            CreateIndex("dbo.RequirementForPayments", "Resolution_0");
            CreateIndex("dbo.RequirementForPayments", "Applicant_ID");
            AddForeignKey("dbo.RequirementForPayments", "Resolution_1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RequirementForPayments", "Resolution_0", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ListOfPayments", "RequirementForPayment_ID1", "dbo.RequirementForPayments", "ID");
            AddForeignKey("dbo.RequirementForPayments", "Contractor_ID1", "dbo.Contractors", "ID");
            AddForeignKey("dbo.RequirementForPayments", "Budget_Item_ID", "dbo.BudgetItems", "ID");
            AddForeignKey("dbo.RequirementForPayments", "Applicant_ID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RequirementForPayments", "Structure_ID1", "dbo.Structures", "ID");
            AddForeignKey("dbo.RolesRFPUsers", "RoleRFPID", "dbo.RolesRFPs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RolesRFPUsers", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
