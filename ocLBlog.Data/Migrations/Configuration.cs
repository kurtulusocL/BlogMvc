namespace ocLBlog.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ocLBlog.Data.Data;
    using ocLBlog.Data.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ocLBlog.Data.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ocLBlog.Data.Data.ApplicationDbContext context)
        {
            //CreateRoles(context);
            //CreateAdmin(context);
        }

        private void CreateAdmin(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = userManager.FindByNameAsync("Admin").Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "kurtulus.ocL",
                    NameLastname = "Kurtuluþ Öcal",
                    Email = "kurtulusocal@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(user, "#ocL.s_1972#");
                userManager.AddToRole(user.Id, "Admin");
            }
            var userInRole = userManager.IsInRole(user.Id, "Admin");
            if (!userInRole)
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }

        private void CreateRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleName = { "Admin", "User" };
            foreach (var role in roleName)
            {
                if (roleManager.RoleExists(role) == false)
                {
                    var newRole = new IdentityRole { Name = role };
                    roleManager.Create(newRole);
                }
            }
        }
    }
}
