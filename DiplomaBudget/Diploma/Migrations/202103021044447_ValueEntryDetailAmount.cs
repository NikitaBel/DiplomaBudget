namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueEntryDetailAmount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ValueEntryDetails", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ValueEntryDetails", "Amount", c => c.Single(nullable: false));
        }
    }
}
