namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostingDateUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Allow_Posting_From");
            DropColumn("dbo.AspNetUsers", "Allow_Posting_To");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Allow_Posting_To", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Allow_Posting_From", c => c.DateTime(nullable: false));
        }
    }
}
