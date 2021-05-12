namespace TaskScript.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TaskScript.Application.Data.Models;

    public class SubjectTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
                .HasIndex(subject => subject.Name)
                .IsUnique();
        }
    }
}
