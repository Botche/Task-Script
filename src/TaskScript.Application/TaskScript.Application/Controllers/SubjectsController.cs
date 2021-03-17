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
            List<SubjectViewModel> subjectsNames = dbContext.Subjects
                .Select(subject => new SubjectViewModel
                {
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

            return View(subjectsViewModel);
        }

        [HttpGet]
        public IActionResult All(int id = 123123, string name = "Default")
        {
            // var queryString = HttpContext.Request.QueryString.Value;

            IdNameViewModel model = new IdNameViewModel()
            {
                Id = id,
                Name = name,
            };

            return View("all", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SubjectBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View("create", model);
            }

            Subject subject = new Subject();
            subject.Name = model.Name;

            dbContext.Subjects.Add(subject);
            dbContext.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Subject subject = dbContext.Subjects
                .Where(subject => subject.Id == id)
                .SingleOrDefault();
                //.SingleOrDefault(subject => subject.Id == id);

            dbContext.Subjects.Remove(subject);
            dbContext.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Subject subject = dbContext.Subjects
                .Where(subject => subject.Id == id)
                .SingleOrDefault();

            Random random = new Random();

            subject.Name = $"ASP {random.Next(0, 10)}";

            dbContext.Subjects.Update(subject);
            dbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
