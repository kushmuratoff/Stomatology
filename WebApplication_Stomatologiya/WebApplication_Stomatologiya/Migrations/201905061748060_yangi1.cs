namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tish",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                        Malumot = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tish");
        }
    }
}
