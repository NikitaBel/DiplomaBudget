namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BudgetItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BudgetNameID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ParentID = c.Int(nullable: false),
                        Operator = c.Short(nullable: false),
                        Calculation = c.String(),
                        RFP = c.Boolean(nullable: false),
                        Level = c.Short(nullable: false),
                        Priority = c.Short(nullable: false),
                        ParentBudget_Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BudgetNames", t => t.BudgetNameID, cascadeDelete: true)
                .ForeignKey("dbo.BudgetItems", t => t.ParentBudget_Item_ID)
                .Index(t => t.BudgetNameID)
                .Index(t => t.ParentBudget_Item_ID);
            
            CreateTable(
                "dbo.BudgetNames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Structures", t => t.Structure_ID1)
                .ForeignKey("dbo.AspNetUsers", t => t.Applicant_ID)
                .ForeignKey("dbo.BudgetItems", t => t.Budget_Item_ID)
                .ForeignKey("dbo.Contractors", t => t.Contractor_ID1)
                .ForeignKey("dbo.AspNetUsers", t => t.Resolution_0)
                .ForeignKey("dbo.AspNetUsers", t => t.Resolution_1)
                .Index(t => t.Applicant_ID)
                .Index(t => t.Resolution_0)
                .Index(t => t.Resolution_1)
                .Index(t => t.Structure_ID1)
                .Index(t => t.Budget_Item_ID)
                .Index(t => t.Contractor_ID1);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Postition = c.String(),
                        Active = c.Boolean(nullable: false),
                        Allow_Posting_From = c.DateTime(nullable: false),
                        Allow_Posting_To = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ValueEntryHeaders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntryType = c.String(),
                        Version_ID = c.Int(nullable: false),
                        Structure_ID = c.Int(nullable: false),
                        BudgetItem_ID = c.Int(nullable: false),
                        PostingDate = c.DateTime(nullable: false),
                        CreatedUserID = c.Int(nullable: false),
                        AmountTotal = c.Single(nullable: false),
                        BudgetItem_ID1 = c.Int(),
                        CreatedUser_Id = c.String(maxLength: 128),
                        Dates_Date = c.DateTime(),
                        Structure_ID1 = c.Int(),
                        Version_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BudgetItems", t => t.BudgetItem_ID1)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUser_Id)
                .ForeignKey("dbo.Dates", t => t.Dates_Date)
                .ForeignKey("dbo.Structures", t => t.Structure_ID1)
                .ForeignKey("dbo.BudgetVersions", t => t.Version_ID1)
                .Index(t => t.BudgetItem_ID1)
                .Index(t => t.CreatedUser_Id)
                .Index(t => t.Dates_Date)
                .Index(t => t.Structure_ID1)
                .Index(t => t.Version_ID1);
            
            CreateTable(
                "dbo.Dates",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        YearName = c.Int(nullable: false),
                        YearDate = c.DateTime(nullable: false),
                        MMMM = c.String(),
                        MMM = c.String(),
                        M = c.Int(nullable: false),
                        BudgetDate = c.String(),
                        StartOfMonth = c.DateTime(nullable: false),
                        EndOfMonth = c.DateTime(nullable: false),
                        QuarterNo = c.Int(nullable: false),
                        QuarterName = c.String(),
                        StartOfQrt = c.DateTime(nullable: false),
                        EndOfQrt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Date);
            
            CreateTable(
                "dbo.ValueEntryDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ValueEntryHeader_ID = c.Int(nullable: false),
                        BudgetDate_ID = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                        Dates_Date = c.DateTime(),
                        ValueEntryHeader_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dates", t => t.Dates_Date)
                .ForeignKey("dbo.ValueEntryHeaders", t => t.ValueEntryHeader_ID1)
                .Index(t => t.Dates_Date)
                .Index(t => t.ValueEntryHeader_ID1);
            
            CreateTable(
                "dbo.Structures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        ParentID = c.Int(nullable: false),
                        Level = c.Short(nullable: false),
                        ParentStructure_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Structures", t => t.ParentStructure_ID)
                .Index(t => t.ParentStructure_ID);
            
            CreateTable(
                "dbo.BudgetVersions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Short(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ApprovedUserID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RequirementForPayments", t => t.RequirementForPayment_ID1)
                .Index(t => t.RequirementForPayment_ID1);
            
            CreateTable(
                "dbo.StructureBudgetItems",
                c => new
                    {
                        Structure_ID = c.Int(nullable: false),
                        BudgetItem_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Structure_ID, t.BudgetItem_ID })
                .ForeignKey("dbo.Structures", t => t.Structure_ID, cascadeDelete: true)
                .ForeignKey("dbo.BudgetItems", t => t.BudgetItem_ID, cascadeDelete: true)
                .Index(t => t.Structure_ID)
                .Index(t => t.BudgetItem_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RequirementForPayments", "Resolution_1", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequirementForPayments", "Resolution_0", "dbo.AspNetUsers");
            DropForeignKey("dbo.ListOfPayments", "RequirementForPayment_ID1", "dbo.RequirementForPayments");
            DropForeignKey("dbo.RequirementForPayments", "Contractor_ID1", "dbo.Contractors");
            DropForeignKey("dbo.RequirementForPayments", "Budget_Item_ID", "dbo.BudgetItems");
            DropForeignKey("dbo.RequirementForPayments", "Applicant_ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetVersions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ValueEntryHeaders", "Version_ID1", "dbo.BudgetVersions");
            DropForeignKey("dbo.ValueEntryHeaders", "Structure_ID1", "dbo.Structures");
            DropForeignKey("dbo.RequirementForPayments", "Structure_ID1", "dbo.Structures");
            DropForeignKey("dbo.Structures", "ParentStructure_ID", "dbo.Structures");
            DropForeignKey("dbo.StructureBudgetItems", "BudgetItem_ID", "dbo.BudgetItems");
            DropForeignKey("dbo.StructureBudgetItems", "Structure_ID", "dbo.Structures");
            DropForeignKey("dbo.ValueEntryHeaders", "Dates_Date", "dbo.Dates");
            DropForeignKey("dbo.ValueEntryDetails", "ValueEntryHeader_ID1", "dbo.ValueEntryHeaders");
            DropForeignKey("dbo.ValueEntryDetails", "Dates_Date", "dbo.Dates");
            DropForeignKey("dbo.ValueEntryHeaders", "CreatedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ValueEntryHeaders", "BudgetItem_ID1", "dbo.BudgetItems");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetItems", "ParentBudget_Item_ID", "dbo.BudgetItems");
            DropForeignKey("dbo.BudgetItems", "BudgetNameID", "dbo.BudgetNames");
            DropIndex("dbo.StructureBudgetItems", new[] { "BudgetItem_ID" });
            DropIndex("dbo.StructureBudgetItems", new[] { "Structure_ID" });
            DropIndex("dbo.ListOfPayments", new[] { "RequirementForPayment_ID1" });
            DropIndex("dbo.BudgetVersions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Structures", new[] { "ParentStructure_ID" });
            DropIndex("dbo.ValueEntryDetails", new[] { "ValueEntryHeader_ID1" });
            DropIndex("dbo.ValueEntryDetails", new[] { "Dates_Date" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Version_ID1" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Structure_ID1" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Dates_Date" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "CreatedUser_Id" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "BudgetItem_ID1" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.RequirementForPayments", new[] { "Contractor_ID1" });
            DropIndex("dbo.RequirementForPayments", new[] { "Budget_Item_ID" });
            DropIndex("dbo.RequirementForPayments", new[] { "Structure_ID1" });
            DropIndex("dbo.RequirementForPayments", new[] { "Resolution_1" });
            DropIndex("dbo.RequirementForPayments", new[] { "Resolution_0" });
            DropIndex("dbo.RequirementForPayments", new[] { "Applicant_ID" });
            DropIndex("dbo.BudgetItems", new[] { "ParentBudget_Item_ID" });
            DropIndex("dbo.BudgetItems", new[] { "BudgetNameID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.StructureBudgetItems");
            DropTable("dbo.ListOfPayments");
            DropTable("dbo.Contractors");
            DropTable("dbo.BudgetVersions");
            DropTable("dbo.Structures");
            DropTable("dbo.ValueEntryDetails");
            DropTable("dbo.Dates");
            DropTable("dbo.ValueEntryHeaders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.RequirementForPayments");
            DropTable("dbo.BudgetNames");
            DropTable("dbo.BudgetItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
