namespace GenerationC.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.devices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        type = c.String(nullable: false, maxLength: 15),
                        gateway = c.String(nullable: false, maxLength: 20),
                        created_at = c.DateTime(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 60),
                        email = c.String(nullable: false, maxLength: 60),
                        username = c.String(nullable: false, maxLength: 15),
                        password = c.String(nullable: false, maxLength: 150),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.devices", "User_Id", "dbo.users");
            DropIndex("dbo.devices", new[] { "User_Id" });
            DropTable("dbo.users");
            DropTable("dbo.devices");
        }
    }
}
