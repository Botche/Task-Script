namespace TaskScript.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using TaskScript.Application.Data;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Models;

    public class SubjectsController : Controller
    {
        private ApplicationDbContext dbContext;

        public SubjectsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<SubjectViewModel> subjectsNames = this.dbContext.Subjects
                .Select(subject => new SubjectViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                })
                //.Where(subject => subject.Name.Contains("ASP"))
                .OrderBy(subject => subject.Name)
                //.Skip(2)
                //.Take(2)
                .ToList();

            string username = "Gosho";
            DateTime timeNow = DateTime.UtcNow;

            SubjectsViewModel subjectsViewModel = new SubjectsViewModel();

            subjectsViewModel.Subjects = subjectsNames;
            subjectsViewModel.Username = username;
            subjectsViewModel.TimeNow = timeNow;

            return this.View(subjectsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SubjectBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            Subject subjectFromDb = this.dbContext.Subjects
                .Where(s => s.Name == model.Name)
                .SingleOrDefault();

            bool isSubjectAlreadyInDb = subjectFromDb != null;
            if (isSubjectAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }

            Subject subject = new Subject();
            subject.Name = model.Name;

            this.dbContext.Subjects.Add(subject);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            SubjectUpdateBindingModel subject = this.dbContext.Subjects
                .Select(s => new SubjectUpdateBindingModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .Where(s => s.Id == id)
                .SingleOrDefault();

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return this.RedirectToAction("index");
            }

            return this.View(subject);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(SubjectUpdateBindingModel model)
        {
            Subject subject = this.dbContext.Subjects
                .Where(s => s.Id == model.Id)
                .SingleOrDefault();

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return this.View(model);
            }

            subject.Name = model.Name;
            this.dbContext.Subjects.Update(subject);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Subject subject = this.dbContext.Subjects
                .Where(subject => subject.Id == id)
                .SingleOrDefault();
            //.SingleOrDefault(subject => subject.Id == id);

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return this.RedirectToAction("index");
            }

            this.dbContext.Subjects.Remove(subject);
            this.dbContext.SaveChanges();

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
