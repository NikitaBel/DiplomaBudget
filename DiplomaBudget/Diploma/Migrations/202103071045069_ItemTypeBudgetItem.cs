namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemTypeBudgetItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetItems", "ItemType", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BudgetItems", "ItemType");
        }
    }
}
