namespace TaskScript.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels;

    public interface ILessonsUsersService
    {
        Task<bool> EnrollUserToLessonAsync(string userId, int lessonId);

        Task<bool> RemoveUserFromLessonAsync(string userId, int lessonId);

        int? SeatsLeftInLesson(int lessonId);

        int SeatsTakenInLesson(int lessonId);

        bool IsAlreadyEnrolledInLesson(string userId, int lessonId);

        IEnumerable<GetAllLessonsViewModel> PopulateLessonsWithInformationAboutUsers(IEnumerable<GetAllLessonsViewModel> lessons, string id);

        LessonViewModel PopulateLessonWithUsersEnrolledInformation(LessonViewModel lesson);
    }
}
