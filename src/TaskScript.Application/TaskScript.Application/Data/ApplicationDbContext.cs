namespace TaskScript.Application.Data
{
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

        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Subject>()
                .HasIndex(subject => subject.Name)
                .IsUnique();

            builder
                .Entity<Lesson>()
                .HasOne(lesson => lesson.Subject)
                .WithMany(subject => subject.Lessons)
                .HasForeignKey(lesson => lesson.SubjectId);
        }
    }
}
