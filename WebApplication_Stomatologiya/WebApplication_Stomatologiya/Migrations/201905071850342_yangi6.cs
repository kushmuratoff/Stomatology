namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korik", "Holat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Korik", "Holat");
        }
    }
}
