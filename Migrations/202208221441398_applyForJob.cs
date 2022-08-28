namespace JobWeb2022.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applyForJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        ApplyForJobId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ApplyForJobId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.JobId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jobs");
            DropIndex("dbo.ApplyForJobs", new[] { "UserId" });
            DropIndex("dbo.ApplyForJobs", new[] { "JobId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
