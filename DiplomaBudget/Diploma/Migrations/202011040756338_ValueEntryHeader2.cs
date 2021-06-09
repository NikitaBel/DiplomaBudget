namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueEntryHeader2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ValueEntryHeaders", new[] { "CreatedUser_Id" });
            DropColumn("dbo.ValueEntryHeaders", "CreatedUserID");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "CreatedUser_Id", newName: "CreatedUserID");
            AlterColumn("dbo.ValueEntryHeaders", "CreatedUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ValueEntryHeaders", "CreatedUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ValueEntryHeaders", new[] { "CreatedUserID" });
            AlterColumn("dbo.ValueEntryHeaders", "CreatedUserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "CreatedUserID", newName: "CreatedUser_Id");
            AddColumn("dbo.ValueEntryHeaders", "CreatedUserID", c => c.Int(nullable: false));
            CreateIndex("dbo.ValueEntryHeaders", "CreatedUser_Id");
        }
    }
}
