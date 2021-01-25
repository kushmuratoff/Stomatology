namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Yangilik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StomatologiyaId = c.Int(),
                        Rasm = c.String(),
                        Mavzu = c.String(),
                        Batafsil = c.String(),
                        Vaqti = c.DateTime(),
                        Holati = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stomatologiya", t => t.StomatologiyaId)
                .Index(t => t.StomatologiyaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yangilik", "StomatologiyaId", "dbo.Stomatologiya");
            DropIndex("dbo.Yangilik", new[] { "StomatologiyaId" });
            DropTable("dbo.Yangilik");
        }
    }
}
