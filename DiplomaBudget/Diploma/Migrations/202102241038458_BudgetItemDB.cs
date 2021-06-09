namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetItemDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetItems", "UnaryOperator", c => c.String());
            AddColumn("dbo.BudgetItems", "MDX_Formula", c => c.String());
            AddColumn("dbo.BudgetItems", "SortOrder", c => c.Short(nullable: false));
            DropColumn("dbo.BudgetItems", "Operator");
            DropColumn("dbo.BudgetItems", "Calculation");
            DropColumn("dbo.BudgetItems", "RFP");
            DropColumn("dbo.BudgetItems", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BudgetItems", "Priority", c => c.Short(nullable: false));
            AddColumn("dbo.BudgetItems", "RFP", c => c.Boolean(nullable: false));
            AddColumn("dbo.BudgetItems", "Calculation", c => c.String());
            AddColumn("dbo.BudgetItems", "Operator", c => c.Short(nullable: false));
            DropColumn("dbo.BudgetItems", "SortOrder");
            DropColumn("dbo.BudgetItems", "MDX_Formula");
            DropColumn("dbo.BudgetItems", "UnaryOperator");
        }
    }
}
