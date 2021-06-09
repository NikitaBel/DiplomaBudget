namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetNames", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.BudgetNames", "ForPlan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BudgetNames", "ForPlan");
            DropColumn("dbo.BudgetNames", "Active");
        }
    }
}
