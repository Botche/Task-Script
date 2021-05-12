namespace TaskScript.Application.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface ILessonsUsersService
    {
        Task<bool> EnrollUserToLessonAsync(string userId, int lessonId);

        Task<bool> RemoveUserFromLessonAsync(string userId, int lessonId);

        Task<int> SeatsLeftInLessonAsync(int lessonId);

        bool IsAlreadyEnrolledInLesson(string userId, int lessonId);
    }
}
