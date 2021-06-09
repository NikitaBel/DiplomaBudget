namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BudgetItems", "ParentID", c => c.Int());
            AlterColumn("dbo.Structures", "ParentID", c => c.Int());
            AlterColumn("dbo.Structures", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Structures", "Level", c => c.Short(nullable: false));
            AlterColumn("dbo.Structures", "ParentID", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetItems", "ParentID", c => c.Int(nullable: false));
        }
    }
}
