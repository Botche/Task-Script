namespace TaskScript.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TaskScript.Application.Data.Models;

    public class LessonUserTypeConfiguration : IEntityTypeConfiguration<LessonUser>
    {
        public void Configure(EntityTypeBuilder<LessonUser> builder)
        {
            builder
                .HasOne(lu => lu.Lesson)
                .WithMany(l => l.LessonsUsers)
                .HasForeignKey(lu => lu.LessonId);

            builder
                .HasOne(lu => lu.User)
                .WithMany(u => u.UsersLessons)
                .HasForeignKey(lu => lu.UserId);
        }
    }
}
