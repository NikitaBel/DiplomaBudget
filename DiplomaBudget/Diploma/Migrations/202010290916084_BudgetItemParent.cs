namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetItemParent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BudgetItems", "ParentID");
            RenameColumn(table: "dbo.BudgetItems", name: "ParentBudget_Item_ID", newName: "ParentID");
            RenameIndex(table: "dbo.BudgetItems", name: "IX_ParentBudget_Item_ID", newName: "IX_ParentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BudgetItems", name: "IX_ParentID", newName: "IX_ParentBudget_Item_ID");
            RenameColumn(table: "dbo.BudgetItems", name: "ParentID", newName: "ParentBudget_Item_ID");
            AddColumn("dbo.BudgetItems", "ParentID", c => c.Int());
        }
    }
}
