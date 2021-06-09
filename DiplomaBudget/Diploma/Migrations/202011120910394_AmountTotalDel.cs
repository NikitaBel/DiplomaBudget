namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmountTotalDel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ValueEntryHeaders", "AmountTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ValueEntryHeaders", "AmountTotal", c => c.Single(nullable: false));
        }
    }
}
