namespace TaskScript.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TaskScript.Application.Data;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Models.Lessons.BindingModels;
    using TaskScript.Application.Models.Lessons.ViewModels;
    using TaskScript.Application.Models.Subjects.ViewModels;
    using TaskScript.Application.Services.Interfaces;

    public class LessonsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ISubjectsService subjectsService;
        private readonly ILessonsService lessonsService;

        public LessonsController(ApplicationDbContext dbContext, ISubjectsService subjectsService, ILessonsService lessonsService)
        {
            this.dbContext = dbContext;
            this.subjectsService = subjectsService;
            this.lessonsService = lessonsService;
        }

        public IActionResult Index()
        {
            IEnumerable<GetAllLessonsViewModel> lessons = this.lessonsService.GetAll();

            return this.View(lessons);
        }

        public IActionResult Details(int id)
        {
            LessonViewModel lesson = this.lessonsService.GetById(id);

            bool isNull = lesson == null;
            if (isNull)
            {
                return this.BadRequest();
            }

            return this.View(lesson);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            IEnumerable<IdNameViewModel> subjects = this.subjectsService.GetAll();

            bool areSubjectsEmpty = subjects.Count() == 0;
            if (areSubjectsEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.Subjects = subjects;

            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLessonBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            await this.lessonsService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            UpdateLessonBindingModel lesson = this.lessonsService.GetByIdForUpdateMethod(id);

            IEnumerable<IdNameViewModel> subjects = this.subjectsService.GetAll();

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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateLessonBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            bool isUpdated = await this.lessonsService.UpdateAsync(model);
            if (isUpdated == false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.lessonsService.DeleteAsync(id);
            if (isDeleted == false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("index");
        }
    }
}
