namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesRFP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RolesRFPUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        RoleRFPID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.RolesRFPs", t => t.RoleRFPID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleRFPID);
            
            CreateTable(
                "dbo.RolesRFPs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesRFPUsers", "RoleRFPID", "dbo.RolesRFPs");
            DropForeignKey("dbo.RolesRFPUsers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.RolesRFPUsers", new[] { "RoleRFPID" });
            DropIndex("dbo.RolesRFPUsers", new[] { "UserID" });
            DropTable("dbo.RolesRFPs");
            DropTable("dbo.RolesRFPUsers");
        }
    }
}
