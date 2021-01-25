namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yani3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoktorVaqti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoktorId = c.Int(),
                        BemorId = c.Int(),
                        Sanasi = c.DateTime(),
                        vaqti = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bemor", t => t.BemorId)
                .ForeignKey("dbo.Doktor", t => t.DoktorId)
                .Index(t => t.DoktorId)
                .Index(t => t.BemorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoktorVaqti", "DoktorId", "dbo.Doktor");
            DropForeignKey("dbo.DoktorVaqti", "BemorId", "dbo.Bemor");
            DropIndex("dbo.DoktorVaqti", new[] { "BemorId" });
            DropIndex("dbo.DoktorVaqti", new[] { "DoktorId" });
            DropTable("dbo.DoktorVaqti");
        }
    }
}
