namespace TaskScript.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using TaskScript.Application.Data;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Models.Lessons.BindingModels;
    using TaskScript.Application.Models.Lessons.ViewModels;
    using TaskScript.Application.Models.Subjects.ViewModels;

    public class LessonsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LessonsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<GetAllLessonsViewModel> lessons = this.dbContext.Lessons
                .Select(lesson => new GetAllLessonsViewModel
                {
                    Id = lesson.Id,
                    Name = lesson.Name,
                })
                .ToList();

            return this.View(lessons);
        }

        public IActionResult Details(int id)
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
                })
                .Where(lesson => lesson.Id == id)
                .SingleOrDefault();

            bool isNull = lesson == null;
            if (isNull)
            {
                return this.BadRequest();
            }

            return this.View(lesson);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<IdNameViewModel> subjects = this.dbContext.Subjects
                .Select(subject => new IdNameViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                })
                .OrderBy(subject => subject.Name)
                .ToList();

            bool areSubjectsEmpty = subjects.Count() == 0;
            if (areSubjectsEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.Subjects = subjects;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateLessonBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Lesson lesson = new Lesson();
            lesson.Name = model.Name;
            lesson.Hours = model.Hours;
            lesson.IsOnline = model.IsOnline;
            lesson.PresentationDate = model.PresentationDate;
            lesson.Seats = model.Seats;
            lesson.SubjectId = model.SubjectId;

            this.dbContext.Lessons.Add(lesson);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        public IActionResult Update(int id)
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

            IEnumerable<IdNameViewModel> subjects = this.dbContext.Subjects
                .Select(subject => new IdNameViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                })
                .OrderBy(subject => subject.Name)
                .ToList();

            bool isLessonNull = lesson == null;
            bool areSubjectsEmpty = subjects.Count() == 0;
            if (isLessonNull || areSubjectsEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.Subjects = subjects;

            return this.View(lesson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateLessonBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            Lesson lesson = this.dbContext.Lessons
                .Where(l => l.Id == model.Id)
                .SingleOrDefault();

            bool isLessonNull = lesson == null;
            if (isLessonNull)
            {
                return this.RedirectToAction("index");
            }

            lesson.Name = model.Name;
            lesson.IsOnline = model.IsOnline;
            lesson.Hours = model.Hours;
            lesson.PresentationDate = model.PresentationDate;
            lesson.Seats = model.Seats;
            lesson.SubjectId = model.SubjectId;

            this.dbContext.Lessons.Update(lesson);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Lesson lesson = this.dbContext.Lessons
                .Where(lesson => lesson.Id == id)
                .SingleOrDefault();

            bool isNull = lesson == null;
            if (isNull)
            {
                return this.BadRequest();
            }

            this.dbContext.Lessons.Remove(lesson);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }
    }
}
