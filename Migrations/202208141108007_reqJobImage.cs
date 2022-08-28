namespace JobWeb2022.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reqJobImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobImage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobImage", c => c.String());
        }
    }
}
