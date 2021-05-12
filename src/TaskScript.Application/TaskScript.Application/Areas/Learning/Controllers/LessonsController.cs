namespace TaskScript.Application.Areas.Learning.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using TaskScript.Application.Areas.Learning.Models.Lessons.BindingModels;
    using TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels;
    using TaskScript.Application.Areas.Learning.Models.Subjects.ViewModels;
    using TaskScript.Application.Constants;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Infrastructure.Filters;
    using TaskScript.Application.Services.Interfaces;

    public class LessonsController : LearningController
    {
        private readonly ISubjectsService subjectsService;
        private readonly ILessonsService lessonsService;
        private readonly ILessonsUsersService lessonsUsersService;
        private readonly UserManager<ApplicationUser> userManager;

        public LessonsController(
            ISubjectsService subjectsService, 
            ILessonsService lessonsService,
            ILessonsUsersService lessonsUsersService,
            UserManager<ApplicationUser> userManager)
        {
            this.subjectsService = subjectsService;
            this.lessonsService = lessonsService;
            this.lessonsUsersService = lessonsUsersService;
            this.userManager = userManager;
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

            this.ViewBag.Subjects = subjects;

            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Create(CreateLessonBindingModel model)
        {
            await this.lessonsService.CreateAsync(model);

            this.TempData[NotificationsConstants.SuccessNotification] = NotificationsConstants.SuccessfullyAddedLesson;
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
        [ModelStateValidationFilter]
        public async Task<IActionResult> Update(UpdateLessonBindingModel model)
        {
            bool isUpdated = await this.lessonsService.UpdateAsync(model);
            if (isUpdated == false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.AdminRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.lessonsService.DeleteAsync(id);
            if (isDeleted == false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("index");
        }

        public async Task<IActionResult> Enroll(int id)
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyEnrolled = await this.lessonsUsersService.EnrollUserToLessonAsync(currentUser.Id, id);
            if (isSuccessfullyEnrolled)
            {
                this.TempData[NotificationsConstants.SuccessNotification] = NotificationsConstants.SuccessfullyEnrolledInLesson;
            }
            else
            {

                this.TempData[NotificationsConstants.WarningNotification] = NotificationsConstants.AlreadyEnrolledInLesson;
            }

            return this.RedirectToAction("index");
        }

        public async Task<IActionResult> Disenroll(int id)
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyDisenrolled = await this.lessonsUsersService.RemoveUserFromLessonAsync(currentUser.Id, id);
            if (isSuccessfullyDisenrolled)
            {
                this.TempData[NotificationsConstants.SuccessNotification] = NotificationsConstants.SuccessfullyDisenrolledFromLesson;
            }
            else
            {

                this.TempData[NotificationsConstants.WarningNotification] = NotificationsConstants.AlreadyDisenrolledFromLesson;
            }

            return this.RedirectToAction("index");
        }
    }
}
