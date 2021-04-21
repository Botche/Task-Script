namespace TaskScript.Application.Data.Seed.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using TaskScript.Application.Constants;
    using TaskScript.Application.Data.Seed.Seeders.Interfaces;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = new IdentityUser
            {
                UserName = UsersConstants.UserUsername,
                Email = UsersConstants.UserEmail,
            };
            IdentityUser admin = new IdentityUser
            {
                UserName = UsersConstants.AdminUsername,
                Email = UsersConstants.AdminEmail,
            };

            await userManager.CreateAsync(user, UsersConstants.UserPassword);
            await userManager.CreateAsync(admin, UsersConstants.AdminPassword);
        
            await userManager.AddToRoleAsync(admin, RolesConstants.AdminRoleName);
            await userManager.AddToRoleAsync(user, RolesConstants.UserRoleName);
        }
    }
}
