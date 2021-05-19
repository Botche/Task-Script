namespace TaskScript.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskScript.Application.Areas.Learning.Models.Lessons.BindingModels;
    using TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels;
    using TaskScript.Application.Constants;
    using TaskScript.Application.Data;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Services.Interfaces;

    public class LessonsService : ILessonsService
    {
        private readonly ApplicationDbContext dbContext;

        public LessonsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GetAllLessonsViewModel> GetAll()
        {
            IEnumerable<GetAllLessonsViewModel> lessons = this.dbContext.Lessons
                .Select(lesson => new GetAllLessonsViewModel
                {
                    Id = lesson.Id,
                    Name = lesson.Name,
                })
                .ToList();

            return lessons;
        }

        public LessonViewModel GetById(int id)
        {
            LessonViewModel lesson = this.dbContext.Lessons
                .Select(lesson => new LessonViewModel
                {
                    Id = lesson.Id,
                    Name = lesson.Name,
                    Hours = lesson.Hours,
                    IsOnline = lesson.IsOnline,
                    PresentationDate = lesson.PresentationDate,
                    Seats = lesson.Seats,
                    SubjectName = lesson.Subject.Name,
                    EnrolledUsernames = new List<string>(),
                })
                .Where(lesson => lesson.Id == id)
                .SingleOrDefault();

            return lesson;
        }

        public UpdateLessonBindingModel GetByIdForUpdateMethod(int id)
        {
            UpdateLessonBindingModel lesson = this.dbContext.Lessons
                .Select(l => new UpdateLessonBindingModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Seats = l.Seats,
                    Hours = l.Hours,
                    IsOnline = l.IsOnline,
                    PresentationDate = l.PresentationDate,
                    SubjectId = l.SubjectId,
                })
                .Where(l => l.Id == id)
                .SingleOrDefault();

            return lesson;
        }

        public async Task<int> CreateAsync(CreateLessonBindingModel model)
        {
            Lesson lesson = new Lesson();
            lesson.Name = model.Name;
            lesson.Hours = model.Hours;
            lesson.IsOnline = model.IsOnline;
            lesson.PresentationDate = model.PresentationDate;
            lesson.Seats = model.Seats;
            lesson.SubjectId = model.SubjectId;

            await this.dbContext.Lessons.AddAsync(lesson);
            await this.dbContext.SaveChangesAsync();

            return lesson.Id;
        }

        public async Task<bool> UpdateAsync(UpdateLessonBindingModel model)
        {
            Lesson lesson = this.GetLessonById(model.Id);

            bool isLessonNull = lesson == null;
            if (isLessonNull)
            {
                return false;
            }

            lesson.Name = model.Name;
            lesson.IsOnline = model.IsOnline;
            lesson.Hours = model.Hours;
            lesson.PresentationDate = model.PresentationDate;
            lesson.Seats = model.Seats;
            lesson.SubjectId = model.SubjectId;

            this.dbContext.Lessons.Update(lesson);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Lesson lesson = this.GetLessonById(id);

            bool isNull = lesson == null;
            if (isNull)
            {
                return false;
            }

            this.dbContext.Lessons.Remove(lesson);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public bool CheckIfLessonExist(int id)
        {
            bool isLessonExist = this.dbContext.Lessons
                .Any(l => l.Id == id);

            return isLessonExist;
        }

        public int? GetAllSeats(int lessonId)
        {
            int? allSeats = this.dbContext.Lessons
                .Where(l => l.Id == lessonId)
                .FirstOrDefault()
                .Seats;

            return allSeats;
        }

        public bool CheckIfLessonIsOld(int lessonId)
        {
            if (this.CheckIfLessonExist(lessonId) == false)
            {
                throw new InvalidOperationException(ExceptionConstants.NotExistingLessonErrorMessage);
            }

            DateTime presentationDate = this.GetLessonById(lessonId).PresentationDate;
            DateTime now = DateTime.Now;

            bool isOld = false;
            if (now.CompareTo(presentationDate) > 0)
            {
                isOld = true;
            }

            return isOld;
        }

        private Lesson GetLessonById(int id)
        {
            Lesson lesson = this.dbContext.Lessons
                .Where(l => l.Id == id)
                .SingleOrDefault();

            return lesson;
        }
    }
}
