namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bemor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Familya = c.String(),
                        Ism = c.String(),
                        Sharif = c.String(),
                        TugVaqti = c.DateTime(nullable: false),
                        PasSeria = c.String(),
                        PasNomer = c.String(),
                        KimTomBer = c.String(),
                        YashManzil = c.String(),
                        TelNomer = c.String(),
                        EskiKasallari = c.String(),
                        UserlarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Userlar", t => t.UserlarId)
                .Index(t => t.UserlarId);
            
            CreateTable(
                "dbo.Korik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoktorId = c.Int(),
                        BemorId = c.Int(),
                        Tashxis = c.String(),
                        Shikoyat = c.String(),
                        KasRivoj = c.String(),
                        LabNatija = c.String(),
                        Tishlash = c.String(),
                        OgizBosh = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bemor", t => t.BemorId)
                .ForeignKey("dbo.Doktor", t => t.DoktorId)
                .Index(t => t.DoktorId)
                .Index(t => t.BemorId);
            
            CreateTable(
                "dbo.Doktor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Familya = c.String(),
                        Ism = c.String(),
                        Sharif = c.String(),
                        TugVaqti = c.DateTime(nullable: false),
                        PasSeria = c.String(),
                        PasNomer = c.String(),
                        KimTomBer = c.String(),
                        Rasm = c.String(),
                        YashManzil = c.String(),
                        TelNomer = c.String(),
                        UserlarId = c.Int(),
                        StomatologiyaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stomatologiya", t => t.StomatologiyaId)
                .ForeignKey("dbo.Userlar", t => t.UserlarId)
                .Index(t => t.UserlarId)
                .Index(t => t.StomatologiyaId);
            
            CreateTable(
                "dbo.Stomatologiya",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                        Logatip = c.String(),
                        TumanId = c.Int(),
                        Manzil = c.String(),
                        TelNomer = c.String(),
                        UserlarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tuman", t => t.TumanId)
                .ForeignKey("dbo.Userlar", t => t.UserlarId)
                .Index(t => t.TumanId)
                .Index(t => t.UserlarId);
            
            CreateTable(
                "dbo.Tuman",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                        ViloyatId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Viloyat", t => t.ViloyatId)
                .Index(t => t.ViloyatId);
            
            CreateTable(
                "dbo.Viloyat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Userlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logini = c.String(),
                        Paroli = c.String(),
                        RollarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rollar", t => t.RollarId)
                .Index(t => t.RollarId);
            
            CreateTable(
                "dbo.Rollar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Xulosa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KorikId = c.Int(),
                        Summa = c.Int(nullable: false),
                        Vaqt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korik", t => t.KorikId)
                .Index(t => t.KorikId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Xulosa", "KorikId", "dbo.Korik");
            DropForeignKey("dbo.Stomatologiya", "UserlarId", "dbo.Userlar");
            DropForeignKey("dbo.Userlar", "RollarId", "dbo.Rollar");
            DropForeignKey("dbo.Doktor", "UserlarId", "dbo.Userlar");
            DropForeignKey("dbo.Bemor", "UserlarId", "dbo.Userlar");
            DropForeignKey("dbo.Tuman", "ViloyatId", "dbo.Viloyat");
            DropForeignKey("dbo.Stomatologiya", "TumanId", "dbo.Tuman");
            DropForeignKey("dbo.Doktor", "StomatologiyaId", "dbo.Stomatologiya");
            DropForeignKey("dbo.Korik", "DoktorId", "dbo.Doktor");
            DropForeignKey("dbo.Korik", "BemorId", "dbo.Bemor");
            DropIndex("dbo.Xulosa", new[] { "KorikId" });
            DropIndex("dbo.Userlar", new[] { "RollarId" });
            DropIndex("dbo.Tuman", new[] { "ViloyatId" });
            DropIndex("dbo.Stomatologiya", new[] { "UserlarId" });
            DropIndex("dbo.Stomatologiya", new[] { "TumanId" });
            DropIndex("dbo.Doktor", new[] { "StomatologiyaId" });
            DropIndex("dbo.Doktor", new[] { "UserlarId" });
            DropIndex("dbo.Korik", new[] { "BemorId" });
            DropIndex("dbo.Korik", new[] { "DoktorId" });
            DropIndex("dbo.Bemor", new[] { "UserlarId" });
            DropTable("dbo.Xulosa");
            DropTable("dbo.Rollar");
            DropTable("dbo.Userlar");
            DropTable("dbo.Viloyat");
            DropTable("dbo.Tuman");
            DropTable("dbo.Stomatologiya");
            DropTable("dbo.Doktor");
            DropTable("dbo.Korik");
            DropTable("dbo.Bemor");
        }
    }
}
