namespace TaskScript.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
            DetailsLessonViewModel lesson = this.dbContext.Lessons
                .Select(lesson => new DetailsLessonViewModel
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
