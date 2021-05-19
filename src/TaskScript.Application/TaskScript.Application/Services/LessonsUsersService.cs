namespace TaskScript.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels;
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
                throw new InvalidOperationException(ExceptionConstants.AlreadyEnrolledInLessonErrorMessage);
            }

            int? seatsLeft = this.SeatsLeftInLesson(lessonId);
            bool canJoin = seatsLeft.HasValue == false || seatsLeft.Value > 0;
            if (canJoin == false)
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

        public int? SeatsLeftInLesson(int lessonId)
        {
            int seatsTaken = this.SeatsTakenInLesson(lessonId);
            int? allSeats = this.lessonsService.GetAllSeats(lessonId);

            if (allSeats.HasValue == false)
            {
                return null;
            }

            int leftSeats = allSeats.Value - seatsTaken;

            return leftSeats;
        }

        public int SeatsTakenInLesson(int lessonId)
        {
            int seatsTaken = this.dbContext.LessonsUsers
                .Where(lu => lu.LessonId == lessonId)
                .Count();

            return seatsTaken;
        }

        public IEnumerable<GetAllLessonsViewModel> PopulateLessonsWithInformationAboutUsers(IEnumerable<GetAllLessonsViewModel> lessons, string userId)
        {
            foreach (GetAllLessonsViewModel lesson in lessons)
            {
                lesson.CurrentUserIsEnrolled = this.IsAlreadyEnrolledInLesson(userId, lesson.Id);
                lesson.SeatsLeft = this.SeatsLeftInLesson(lesson.Id);
            }

            return lessons;
        }

        public LessonViewModel PopulateLessonWithUsersEnrolledInformation(LessonViewModel lesson)
        {
            lesson.SeatsLeft = this.SeatsLeftInLesson(lesson.Id);
            lesson.EnrolledUsernames = this.GetAllUsernamesThatEnrollInLesson(lesson.Id);

            return lesson;
        }

        public bool CheckIfSeatsValueIsPositiveBasedOnAlreadyEnrolledUsers(int id, int? newSeats)
        {
            if (this.lessonsService.CheckIfLessonExist(id) == false)
            {
                throw new InvalidOperationException(ExceptionConstants.NotExistingLessonErrorMessage);
            }

            int seatsTaken = this.SeatsTakenInLesson(id);

            bool isCorrect = newSeats.HasValue == false || seatsTaken <= newSeats.Value;
            if (isCorrect == false)
            {
                return false;
            }

            return true;
        }

        private IEnumerable<string> GetAllUsernamesThatEnrollInLesson(int lessonId)
        {
            IEnumerable<string> usernames = this.dbContext.LessonsUsers
                .Where(lu => lu.LessonId == lessonId)
                .Select(lu => lu.User.UserName)
                .ToList();

            return usernames;
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
            bool isLessonExists = this.lessonsService.CheckIfLessonExist(lessonId);

            if (isLessonExists == false)
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
