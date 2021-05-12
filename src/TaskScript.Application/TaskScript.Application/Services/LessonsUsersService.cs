namespace TaskScript.Application.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using TaskScript.Application.Constants;
    using TaskScript.Application.Data;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Services.Interfaces;

    public class LessonsUsersService : ILessonsUsersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILessonsService lessonsService;

        public LessonsUsersService(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ILessonsService lessonsService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.lessonsService = lessonsService;
        }

        public async Task<bool> EnrollUserToLessonAsync(string userId, int lessonId)
        {
            await CheckIfUserAndLessonExistsAsync(userId, lessonId);

            if (this.IsAlreadyEnrolledInLesson(userId, lessonId))
            {
                return false;
            }

            LessonUser lessonUser = new LessonUser()
            {
                UserId = userId,
                LessonId = lessonId,
                EnrollmentDate = DateTime.UtcNow,
            };

            await this.dbContext.LessonsUsers.AddAsync(lessonUser);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public bool IsAlreadyEnrolledInLesson(string userId, int lessonId)
        {
            LessonUser enrollment = this.GetEnrollment(userId, lessonId);

            bool isAlreadyEnrolled = enrollment != null;
            return isAlreadyEnrolled;
        }

        public async Task<bool> RemoveUserFromLessonAsync(string userId, int lessonId)
        {
            await CheckIfUserAndLessonExistsAsync(userId, lessonId);

            if (this.IsAlreadyEnrolledInLesson(userId, lessonId) == false)
            {
                return false;
            }

            LessonUser enrollment = this.GetEnrollment(userId, lessonId);

            this.dbContext.LessonsUsers.Remove(enrollment);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<int> SeatsLeftInLessonAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        private LessonUser GetEnrollment(string userId, int lessonId)
        {
            LessonUser enrollment = this.dbContext.LessonsUsers
                .Where(lu => lu.UserId == userId && lu.LessonId == lessonId)
                .FirstOrDefault();

            return enrollment;
        }

        private async Task CheckIfUserAndLessonExistsAsync(string userId, int lessonId)
        {
            if (this.lessonsService.CheckIfLessonExist(lessonId) == false)
            {
                throw new ArgumentException(ExceptionConstants.NotExistingLessonErrorMessage);
            }

            if (await this.userManager.FindByIdAsync(userId) == null)
            {
                throw new ArgumentException(ExceptionConstants.NotExistingUserErrorMessage);
            }
        }
    }
}
