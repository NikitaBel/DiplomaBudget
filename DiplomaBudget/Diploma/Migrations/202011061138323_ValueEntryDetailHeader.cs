namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueEntryDetailHeader : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ValueEntryDetails", "ValueEntryHeader_ID1", "dbo.ValueEntryHeaders");
            DropIndex("dbo.ValueEntryDetails", new[] { "ValueEntryHeader_ID1" });
            DropColumn("dbo.ValueEntryDetails", "ValueEntryHeader_ID");
            RenameColumn(table: "dbo.ValueEntryDetails", name: "ValueEntryHeader_ID1", newName: "ValueEntryHeader_ID");
            AlterColumn("dbo.ValueEntryDetails", "ValueEntryHeader_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.ValueEntryDetails", "ValueEntryHeader_ID");
            AddForeignKey("dbo.ValueEntryDetails", "ValueEntryHeader_ID", "dbo.ValueEntryHeaders", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueEntryDetails", "ValueEntryHeader_ID", "dbo.ValueEntryHeaders");
            DropIndex("dbo.ValueEntryDetails", new[] { "ValueEntryHeader_ID" });
            AlterColumn("dbo.ValueEntryDetails", "ValueEntryHeader_ID", c => c.Int());
            RenameColumn(table: "dbo.ValueEntryDetails", name: "ValueEntryHeader_ID", newName: "ValueEntryHeader_ID1");
            AddColumn("dbo.ValueEntryDetails", "ValueEntryHeader_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.ValueEntryDetails", "ValueEntryHeader_ID1");
            AddForeignKey("dbo.ValueEntryDetails", "ValueEntryHeader_ID1", "dbo.ValueEntryHeaders", "ID");
        }
    }
}
