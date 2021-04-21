namespace TaskScript.Application.Data.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using TaskScript.Application.Data.Seed.Seeders;
    using TaskScript.Application.Data.Seed.Seeders.Interfaces;

    public class ApplicationDbContextSeeder
    {
        private readonly IServiceProvider serviceProvider;

        public ApplicationDbContextSeeder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SeedDatabaseAsync()
        {
            ApplicationDbContext dbContext = this.serviceProvider.GetRequiredService<ApplicationDbContext>();

            List<ISeeder> seeders = new List<ISeeder>()
            {
                new RolesSeeder(),
                new UsersSeeder(),
            };

            foreach (ISeeder seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, this.serviceProvider);
            }
        }
    }
}
