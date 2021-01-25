namespace WebApplication_Stomatologiya.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoktorVaqti", "holati", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoktorVaqti", "holati");
        }
    }
}
