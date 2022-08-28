
using JobWeb2022.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;


[assembly: OwinStartupAttribute(typeof(JobWeb2022.Startup))]
namespace JobWeb2022
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "DuaaFMA";
                user.Email = "duaa.albsharat1992@gmail.com";
                var check = userManager.Create(user, "Duaaf$1992");
                if (check.Succeeded)
                {
                    userManager.AddToRoleAsync(user.Id, "Admin");
                }
            }
        }
    }
}
