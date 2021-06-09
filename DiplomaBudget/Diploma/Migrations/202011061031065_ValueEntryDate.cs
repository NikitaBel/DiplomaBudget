namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueEntryDate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ValueEntryHeaders", "Dates_Date", "dbo.Dates");
            DropIndex("dbo.ValueEntryHeaders", new[] { "Dates_Date" });
            DropColumn("dbo.ValueEntryHeaders", "PostingDate");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "Dates_Date", newName: "PostingDate");
            AlterColumn("dbo.ValueEntryHeaders", "PostingDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.ValueEntryHeaders", "PostingDate");
            AddForeignKey("dbo.ValueEntryHeaders", "PostingDate", "dbo.Dates", "Date", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueEntryHeaders", "PostingDate", "dbo.Dates");
            DropIndex("dbo.ValueEntryHeaders", new[] { "PostingDate" });
            AlterColumn("dbo.ValueEntryHeaders", "PostingDate", c => c.DateTime());
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "PostingDate", newName: "Dates_Date");
            AddColumn("dbo.ValueEntryHeaders", "PostingDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.ValueEntryHeaders", "Dates_Date");
            AddForeignKey("dbo.ValueEntryHeaders", "Dates_Date", "dbo.Dates", "Date");
        }
    }
}
