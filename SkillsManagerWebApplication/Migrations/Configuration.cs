namespace SkillsManagerWebApplication.Migrations
{
    using PwdHasher;
    using SkillsManagerWebApplication.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<SkillsManagerWebApplication.DAL.SkillDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SkillsManagerWebApplication.DAL.SkillDBContext context)
        {
            WebSecurity.InitializeDatabaseConnection("SkillDBContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            if (!roles.RoleExists("Administrator"))
            {
                roles.CreateRole("Administrator");
            }
            if (!roles.RoleExists("Employee"))
            {
                roles.CreateRole("Employee");
            }

            var technologies = new List<Technology>
            {
                new Technology {Name = ".Net",        Description = "Microsoft Programming Technology", },
                new Technology {Name = "Java",        Description = "Java the Code", },
                new Technology {Name = "PHP",         Description = "Program Heroical Programs", },
                new Technology {Name = "Cobol",       Description = "Banking", },
                new Technology {Name = "Javascript",  Description = "Magic Stuff", },
                new Technology {Name = "HTML5",       Description = "Look and Feel", },
                new Technology {Name = "Ruby",        Description = "Ruby on Rails", }
            };
            technologies.ForEach(s => context.Technologies.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var employees = new List<UserProfile>
            {
                new UserProfile { UserName = "johndoe", FullName = "John Doe", Phone = "4345678", 
                    Email = "johndoe@gmail.com", Dni = "33456790", Password = Helpers.SHA1.Encode("12345678"), ConfirmPassword = Helpers.SHA1.Encode("12345678") },
                new UserProfile { UserName = "peterobrien", FullName = "Peter O’Brien", Phone = "4658709",    
                    Email = "peterobrien@gmail.com", Dni = "33456750", Password = Helpers.SHA1.Encode("12345678"), ConfirmPassword = Helpers.SHA1.Encode("12345678") },
                new UserProfile { UserName = "hanssolo", FullName = "Hans Solo", Phone = "4765890",     
                    Email = "hanssolo@gmail.com", Dni = "33456780", Password = Helpers.SHA1.Encode("12345678"), ConfirmPassword = Helpers.SHA1.Encode("12345678") },
                new UserProfile { UserName = "michaelmoore", FullName = "Michael Moore", Phone = "4565656", 
                    Email = "michaelmoore@gmail.com", Dni = "33456290", Password = Helpers.SHA1.Encode("12345678"), ConfirmPassword = Helpers.SHA1.Encode("12345678") },
                new UserProfile { UserName = "jamesbond", FullName = "James Bond", Phone = "4897609",        
                    Email = "jamesbond@gmail.com", Dni = "33456730", Password = Helpers.SHA1.Encode("12345678"), ConfirmPassword = Helpers.SHA1.Encode("12345678") },
                new UserProfile { UserName = "juliaroberts", FullName = "Julia Roberts", Phone = "4657887",   
                    Email = "juliaroberts@gmail.com", Dni = "33356790", Password = Helpers.SHA1.Encode("12345678"), ConfirmPassword = Helpers.SHA1.Encode("12345678") },
                new UserProfile { UserName = "brucedickinson", FullName = "Bruce Dickinson", Phone = "4879056",    
                    Email = "brucedickinson@gmail.com", Dni = "39456790", Password = Helpers.SHA1.Encode("12345678") , ConfirmPassword = Helpers.SHA1.Encode("12345678") },
            };
            employees.ForEach(s => context.UserProfiles.AddOrUpdate(p => p.UserName, s));
            context.SaveChanges();

            var administrators = new List<UserProfile>
            {
                new UserProfile { UserName = "administrator", FullName = "Skills App Administrator", Phone = "4879056",    
                    Email = "skillseveris@gmail.com", Dni = "39456790", Password = Helpers.SHA1.Encode("a1D3g5z0") , ConfirmPassword = Helpers.SHA1.Encode("a1D3g5z0") },
            };

            administrators.ForEach(s => context.UserProfiles.AddOrUpdate(p => p.UserName, s));
            context.SaveChanges();

            roles.AddUsersToRoles(new[] { "administrator" }, new[] { "Administrator" });
            roles.AddUsersToRoles(new[] { "johndoe" }, new[] { "Employee" });
            roles.AddUsersToRoles(new[] { "peterobrien" }, new[] { "Employee" });
            roles.AddUsersToRoles(new[] { "hanssolo" }, new[] { "Employee" });
            roles.AddUsersToRoles(new[] { "michaelmoore" }, new[] { "Employee" });
            roles.AddUsersToRoles(new[] { "jamesbond" }, new[] { "Employee" });
            roles.AddUsersToRoles(new[] { "juliaroberts" }, new[] { "Employee" });
            roles.AddUsersToRoles(new[] { "brucedickinson" }, new[] { "Employee" });
        }
    }
}
