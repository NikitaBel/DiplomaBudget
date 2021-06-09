namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForPlanItem2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BudgetNames", "ForPlan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BudgetNames", "ForPlan", c => c.Boolean(nullable: false));
        }
    }
}
