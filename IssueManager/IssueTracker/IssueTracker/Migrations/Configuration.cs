namespace IssueTracker.Migrations
{
    using IssueTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IssueTracker.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IssueTracker.Models.ApplicationDbContext";
        }

        protected override void Seed(IssueTracker.Models.MyDbContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (RoleManager.RoleExists("admin"))
            {
                RoleManager.Create(new IdentityRole("admin"));
            }
            if (RoleManager.RoleExists("agent"))
            {
                RoleManager.Create(new IdentityRole("agent"));
            }
            if (RoleManager.RoleExists("user"))
            {
                RoleManager.Create(new IdentityRole("user"));
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var admin = new ApplicationUser { UserName = "admin", Email = "admin@andreea.com" };
                UserManager.Create(admin, "Test1!");
                UserManager.AddToRole(admin.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "agent"))
            {
                var agent = new ApplicationUser { UserName = "agent", Email = "agent@andreea.com" };
                UserManager.Create(agent, "Test1!");
                UserManager.AddToRole(agent.Id, "agent");
            }
            if (!context.Users.Any(u => u.UserName == "user"))
            {
                var user = new ApplicationUser { UserName = "user", Email = "user@andreea.com" };

                UserManager.Create(user, "Test1!");
                UserManager.AddToRole(user.Id, "user");
            }
        }
    }
}
