namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueEntryDetailDate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ValueEntryDetails", "Dates_Date", "dbo.Dates");
            DropIndex("dbo.ValueEntryDetails", new[] { "Dates_Date" });
            RenameColumn(table: "dbo.ValueEntryDetails", name: "Dates_Date", newName: "BudgetDateID");
            AlterColumn("dbo.ValueEntryDetails", "BudgetDateID", c => c.DateTime(nullable: false));
            CreateIndex("dbo.ValueEntryDetails", "BudgetDateID");
            AddForeignKey("dbo.ValueEntryDetails", "BudgetDateID", "dbo.Dates", "Date", cascadeDelete: true);
            DropColumn("dbo.ValueEntryDetails", "BudgetDate_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ValueEntryDetails", "BudgetDate_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ValueEntryDetails", "BudgetDateID", "dbo.Dates");
            DropIndex("dbo.ValueEntryDetails", new[] { "BudgetDateID" });
            AlterColumn("dbo.ValueEntryDetails", "BudgetDateID", c => c.DateTime());
            RenameColumn(table: "dbo.ValueEntryDetails", name: "BudgetDateID", newName: "Dates_Date");
            CreateIndex("dbo.ValueEntryDetails", "Dates_Date");
            AddForeignKey("dbo.ValueEntryDetails", "Dates_Date", "dbo.Dates", "Date");
        }
    }
}
