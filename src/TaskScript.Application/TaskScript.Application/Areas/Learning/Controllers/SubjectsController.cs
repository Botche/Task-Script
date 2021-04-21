namespace TaskScript.Application.Areas.Learning.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Models.Subjects.BindingModels;
    using TaskScript.Application.Models.Subjects.ViewModels;
    using TaskScript.Application.Services.Interfaces;

    public class SubjectsController : LearningController
    {
        private readonly ISubjectsService subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
            this.subjectsService = subjectsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<IdNameViewModel> subjects = this.subjectsService.GetAll();

            string username = "Gosho";
            DateTime timeNow = DateTime.UtcNow;

            SubjectsViewModel subjectsViewModel = new SubjectsViewModel();

            subjectsViewModel.Subjects = subjects;
            subjectsViewModel.Username = username;
            subjectsViewModel.TimeNow = timeNow;

            return this.View(subjectsViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            SubjectViewModel subject = this.subjectsService.GetForViewById(id);

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return this.RedirectToRoute("index");
            }

            return this.View(subject);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SubjectBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            Subject subjectFromDb = this.subjectsService.GetByName(model.Name);

            bool isSubjectAlreadyInDb = subjectFromDb != null;
            if (isSubjectAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }

            await this.subjectsService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(int id)
        {
            SubjectViewModel subject = this.subjectsService.GetForViewById(id);

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return this.RedirectToAction("index");
            }

            return this.View(subject);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(SubjectUpdateBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            await this.subjectsService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.subjectsService.RemoveAsync(id);

            return this.RedirectToAction("index");
        }

        //[HttpGet]
        //public IActionResult All(int id = 123123, string name = "Default")
        //{
        //    // var queryString = HttpContext.Request.QueryString.Value;

        //    IdNameViewModel model = new IdNameViewModel()
        //    {
        //        Id = id,
        //        Name = name,
        //    };

        //    return this.View("all", model);
        //}
    }
}
