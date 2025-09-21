using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Project.seedingData
{
    public static class SeedDb
    {
        public static async Task seedingDataAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure role exists
            var roleName = "Admine";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Admin user info
            var username = "MohamedAhmed";
            var email = "MohamedAhmed@gmail.com";
            var password = "Mohamed123&";

            // Ensure user exists
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var user = new User
                {
                    Email = email,
                    UserName = username,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
