namespace SkillsManagerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        FullName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Dni = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        TechnologyID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        Level = c.String(),
                        UserProfile_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.SkillID)
                .ForeignKey("dbo.Technology", t => t.TechnologyID, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId)
                .Index(t => t.TechnologyID)
                .Index(t => t.UserProfile_UserId);
            
            CreateTable(
                "dbo.Technology",
                c => new
                    {
                        TechnologyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TechnologyID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Skill", new[] { "UserProfile_UserId" });
            DropIndex("dbo.Skill", new[] { "TechnologyID" });
            DropForeignKey("dbo.Skill", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Skill", "TechnologyID", "dbo.Technology");
            DropTable("dbo.Technology");
            DropTable("dbo.Skill");
            DropTable("dbo.UserProfile");
        }
    }
}
