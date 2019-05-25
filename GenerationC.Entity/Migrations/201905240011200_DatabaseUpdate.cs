namespace GenerationC.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.devices", "created_at", c => c.DateTime());
            AlterColumn("dbo.users", "created_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.users", "created_at", c => c.DateTime());
            AlterColumn("dbo.devices", "created_at", c => c.DateTime());
        }
    }
}
