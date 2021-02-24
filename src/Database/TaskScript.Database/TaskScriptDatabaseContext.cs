namespace TaskScript.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using TaskScript.Database.Models.Subjects;
    using TaskScript.Database.Models.Tasks;

    public class TaskScriptDatabaseContext : IdentityDbContext
    {
        private DbContextOptions options;

        public TaskScriptDatabaseContext(DbContextOptions options)
            : base(options)
        {
            this.options = options;
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Database=TaskScript;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
