using IssueTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssueTracker.App_Start
{
    public class IssueTrackerInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            RoleManager.Create(new IdentityRole("admin"));
            RoleManager.Create(new IdentityRole("agent"));
            RoleManager.Create(new IdentityRole("user"));

            UserManager.Create(new ApplicationUser() { UserName = "user@andreea.com" });

        }
    }
}