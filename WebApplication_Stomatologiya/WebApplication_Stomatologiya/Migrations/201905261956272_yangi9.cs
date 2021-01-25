namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BotId = c.String(),
                        StatusId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bot");
        }
    }
}
