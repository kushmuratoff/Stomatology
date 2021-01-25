namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IshVaqti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KunId = c.Int(),
                        DoktorId = c.Int(),
                        KelishV = c.String(),
                        KetishV = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doktor", t => t.DoktorId)
                .ForeignKey("dbo.Kun", t => t.KunId)
                .Index(t => t.KunId)
                .Index(t => t.DoktorId);
            
            CreateTable(
                "dbo.Kun",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IshVaqti", "KunId", "dbo.Kun");
            DropForeignKey("dbo.IshVaqti", "DoktorId", "dbo.Doktor");
            DropIndex("dbo.IshVaqti", new[] { "DoktorId" });
            DropIndex("dbo.IshVaqti", new[] { "KunId" });
            DropTable("dbo.Kun");
            DropTable("dbo.IshVaqti");
        }
    }
}
