namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserStructure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StructureID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ParentID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "StructureID");
            CreateIndex("dbo.AspNetUsers", "ParentID");
            AddForeignKey("dbo.AspNetUsers", "ParentID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "StructureID", "dbo.Structures", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StructureID", "dbo.Structures");
            DropForeignKey("dbo.AspNetUsers", "ParentID", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ParentID" });
            DropIndex("dbo.AspNetUsers", new[] { "StructureID" });
            DropColumn("dbo.AspNetUsers", "ParentID");
            DropColumn("dbo.AspNetUsers", "StructureID");
        }
    }
}
