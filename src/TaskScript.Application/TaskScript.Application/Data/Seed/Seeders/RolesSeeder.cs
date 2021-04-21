namespace TaskScript.Application.Data.Seed.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using TaskScript.Application.Constants;
    using TaskScript.Application.Data.Seed.Seeders.Interfaces;

    public class RolesSeeder : ISeeder
    {
        public RolesSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityRole userRole = new IdentityRole
            {
                Name = RolesConstants.UserRoleName,
            };
            IdentityRole adminRole = new IdentityRole
            {
                Name = RolesConstants.AdminRoleName,
            };

            await roleManager.CreateAsync(userRole);
            await roleManager.CreateAsync(adminRole);
        }
    }
}
