using Application.Enums;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                await roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }
    }
}
