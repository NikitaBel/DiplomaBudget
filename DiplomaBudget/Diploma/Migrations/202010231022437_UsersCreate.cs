namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "StructureID", "dbo.Structures");
            DropIndex("dbo.AspNetUsers", new[] { "StructureID" });
            AddColumn("dbo.AspNetUsers", "Position", c => c.String());
            AlterColumn("dbo.AspNetUsers", "StructureID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "StructureID");
            AddForeignKey("dbo.AspNetUsers", "StructureID", "dbo.Structures", "ID");
            DropColumn("dbo.AspNetUsers", "Postition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Postition", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "StructureID", "dbo.Structures");
            DropIndex("dbo.AspNetUsers", new[] { "StructureID" });
            AlterColumn("dbo.AspNetUsers", "StructureID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Position");
            CreateIndex("dbo.AspNetUsers", "StructureID");
            AddForeignKey("dbo.AspNetUsers", "StructureID", "dbo.Structures", "ID", cascadeDelete: true);
        }
    }
}
