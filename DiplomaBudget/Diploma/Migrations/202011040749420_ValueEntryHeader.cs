namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueEntryHeader : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ValueEntryHeaders", "BudgetItem_ID1", "dbo.BudgetItems");
            DropForeignKey("dbo.ValueEntryHeaders", "Structure_ID1", "dbo.Structures");
            DropForeignKey("dbo.ValueEntryHeaders", "Version_ID1", "dbo.BudgetVersions");
            DropIndex("dbo.ValueEntryHeaders", new[] { "BudgetItem_ID1" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Structure_ID1" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Version_ID1" });
            DropColumn("dbo.ValueEntryHeaders", "BudgetItem_ID");
            DropColumn("dbo.ValueEntryHeaders", "Structure_ID");
            DropColumn("dbo.ValueEntryHeaders", "Version_ID");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "BudgetItem_ID1", newName: "BudgetItem_ID");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "Structure_ID1", newName: "Structure_ID");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "Version_ID1", newName: "Version_ID");
            AlterColumn("dbo.ValueEntryHeaders", "BudgetItem_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ValueEntryHeaders", "Structure_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ValueEntryHeaders", "Version_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.ValueEntryHeaders", "Version_ID");
            CreateIndex("dbo.ValueEntryHeaders", "Structure_ID");
            CreateIndex("dbo.ValueEntryHeaders", "BudgetItem_ID");
            AddForeignKey("dbo.ValueEntryHeaders", "BudgetItem_ID", "dbo.BudgetItems", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ValueEntryHeaders", "Structure_ID", "dbo.Structures", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ValueEntryHeaders", "Version_ID", "dbo.BudgetVersions", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueEntryHeaders", "Version_ID", "dbo.BudgetVersions");
            DropForeignKey("dbo.ValueEntryHeaders", "Structure_ID", "dbo.Structures");
            DropForeignKey("dbo.ValueEntryHeaders", "BudgetItem_ID", "dbo.BudgetItems");
            DropIndex("dbo.ValueEntryHeaders", new[] { "BudgetItem_ID" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Structure_ID" });
            DropIndex("dbo.ValueEntryHeaders", new[] { "Version_ID" });
            AlterColumn("dbo.ValueEntryHeaders", "Version_ID", c => c.Int());
            AlterColumn("dbo.ValueEntryHeaders", "Structure_ID", c => c.Int());
            AlterColumn("dbo.ValueEntryHeaders", "BudgetItem_ID", c => c.Int());
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "Version_ID", newName: "Version_ID1");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "Structure_ID", newName: "Structure_ID1");
            RenameColumn(table: "dbo.ValueEntryHeaders", name: "BudgetItem_ID", newName: "BudgetItem_ID1");
            AddColumn("dbo.ValueEntryHeaders", "Version_ID", c => c.Int(nullable: false));
            AddColumn("dbo.ValueEntryHeaders", "Structure_ID", c => c.Int(nullable: false));
            AddColumn("dbo.ValueEntryHeaders", "BudgetItem_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.ValueEntryHeaders", "Version_ID1");
            CreateIndex("dbo.ValueEntryHeaders", "Structure_ID1");
            CreateIndex("dbo.ValueEntryHeaders", "BudgetItem_ID1");
            AddForeignKey("dbo.ValueEntryHeaders", "Version_ID1", "dbo.BudgetVersions", "ID");
            AddForeignKey("dbo.ValueEntryHeaders", "Structure_ID1", "dbo.Structures", "ID");
            AddForeignKey("dbo.ValueEntryHeaders", "BudgetItem_ID1", "dbo.BudgetItems", "ID");
        }
    }
}
