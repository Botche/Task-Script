namespace TaskScript.Application.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using TaskScript.Application.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder
                    .UseSqlServer("Server=.;Database=TaskScript;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Subject>()
                .Property(s => s.Name)
                .IsRequired();
        }
    }
}
