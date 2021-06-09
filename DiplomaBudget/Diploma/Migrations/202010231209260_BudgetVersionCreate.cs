namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetVersionCreate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BudgetVersions", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.BudgetVersions", "ApprovedUserID");
            RenameColumn(table: "dbo.BudgetVersions", name: "ApplicationUser_Id", newName: "ApprovedUserID");
            AlterColumn("dbo.BudgetVersions", "Status", c => c.String());
            AlterColumn("dbo.BudgetVersions", "ApprovedUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.BudgetVersions", "ApprovedUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BudgetVersions", new[] { "ApprovedUserID" });
            AlterColumn("dbo.BudgetVersions", "ApprovedUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetVersions", "Status", c => c.Short(nullable: false));
            RenameColumn(table: "dbo.BudgetVersions", name: "ApprovedUserID", newName: "ApplicationUser_Id");
            AddColumn("dbo.BudgetVersions", "ApprovedUserID", c => c.Int(nullable: false));
            CreateIndex("dbo.BudgetVersions", "ApplicationUser_Id");
        }
    }
}
