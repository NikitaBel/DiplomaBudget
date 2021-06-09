namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureBudgetItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StructureBudgetItems", "Structure_ID", "dbo.Structures");
            DropForeignKey("dbo.StructureBudgetItems", "BudgetItem_ID", "dbo.BudgetItems");
            DropIndex("dbo.StructureBudgetItems", new[] { "Structure_ID" });
            DropIndex("dbo.StructureBudgetItems", new[] { "BudgetItem_ID" });
            CreateTable(
                "dbo.StructureBudgetItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StructureID = c.Int(nullable: false),
                        BudgetItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BudgetItems", t => t.BudgetItemID, cascadeDelete: true)
                .ForeignKey("dbo.Structures", t => t.StructureID, cascadeDelete: true)
                .Index(t => t.StructureID)
                .Index(t => t.BudgetItemID);
            
            DropTable("dbo.StructureBudgetItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StructureBudgetItems",
                c => new
                    {
                        Structure_ID = c.Int(nullable: false),
                        BudgetItem_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Structure_ID, t.BudgetItem_ID });
            
            DropForeignKey("dbo.StructureBudgetItem", "StructureID", "dbo.Structures");
            DropForeignKey("dbo.StructureBudgetItem", "BudgetItemID", "dbo.BudgetItems");
            DropIndex("dbo.StructureBudgetItem", new[] { "BudgetItemID" });
            DropIndex("dbo.StructureBudgetItem", new[] { "StructureID" });
            DropTable("dbo.StructureBudgetItem");
            CreateIndex("dbo.StructureBudgetItems", "BudgetItem_ID");
            CreateIndex("dbo.StructureBudgetItems", "Structure_ID");
            AddForeignKey("dbo.StructureBudgetItems", "BudgetItem_ID", "dbo.BudgetItems", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StructureBudgetItems", "Structure_ID", "dbo.Structures", "ID", cascadeDelete: true);
        }
    }
}
