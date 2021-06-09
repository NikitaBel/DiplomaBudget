namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetItemCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BudgetItems", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BudgetItems", "Level", c => c.Short(nullable: false));
        }
    }
}
