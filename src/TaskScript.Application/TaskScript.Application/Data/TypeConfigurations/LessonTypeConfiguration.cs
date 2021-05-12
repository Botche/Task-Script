namespace TaskScript.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TaskScript.Application.Data.Models;

    public class LessonTypeConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder
                .HasOne(lesson => lesson.Subject)
                .WithMany(subject => subject.Lessons)
                .HasForeignKey(lesson => lesson.SubjectId);
        }
    }
}
