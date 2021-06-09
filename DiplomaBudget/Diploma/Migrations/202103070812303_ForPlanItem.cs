namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForPlanItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetItems", "ForPlan", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BudgetItems", "ForPlan");
        }
    }
}
