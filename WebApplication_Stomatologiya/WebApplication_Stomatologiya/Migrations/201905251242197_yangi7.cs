namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BemorgaXabar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BemorId = c.Int(),
                        Holati = c.Int(nullable: false),
                        Matni = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bemor", t => t.BemorId)
                .Index(t => t.BemorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BemorgaXabar", "BemorId", "dbo.Bemor");
            DropIndex("dbo.BemorgaXabar", new[] { "BemorId" });
            DropTable("dbo.BemorgaXabar");
        }
    }
}
