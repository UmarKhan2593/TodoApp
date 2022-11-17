using Application.Enums;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User

            var defaultUser = new ApplicationUser
            {
                UserName = "umer",
                Email = "umerkhan@gmail.com",
                FirstName = "umer",
                LastName = "khan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Drinkbo@rd!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                }
            }
        }


    }
}
