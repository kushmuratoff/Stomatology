namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korik", "Summa", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Korik", "Summa");
        }
    }
}
