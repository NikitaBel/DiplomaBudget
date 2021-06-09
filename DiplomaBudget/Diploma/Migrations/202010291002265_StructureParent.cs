namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureParent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Structures", "ParentID");
            RenameColumn(table: "dbo.Structures", name: "ParentStructure_ID", newName: "ParentID");
            RenameIndex(table: "dbo.Structures", name: "IX_ParentStructure_ID", newName: "IX_ParentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Structures", name: "IX_ParentID", newName: "IX_ParentStructure_ID");
            RenameColumn(table: "dbo.Structures", name: "ParentID", newName: "ParentStructure_ID");
            AddColumn("dbo.Structures", "ParentID", c => c.Int());
        }
    }
}
