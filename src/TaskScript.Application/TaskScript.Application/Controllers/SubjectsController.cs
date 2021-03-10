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
            SubjectsViewModel subjectsViewModel = new SubjectsViewModel();

            List<SubjectViewModel> subjectsNames = dbContext.Subjects
                .Select(subject => new SubjectViewModel
                {
                    Name = subject.Name,
                })
                .Where(subject => subject.Name.Contains("ASP"))
                .OrderBy(subject => subject.Name)
                //.Skip(2)
                //.Take(2)
                .ToList();

            string username = "Gosho";
            DateTime timeNow = DateTime.UtcNow;

            subjectsViewModel.Subjects = subjectsNames;
            subjectsViewModel.Username = username;
            subjectsViewModel.TimeNow = timeNow;

            return View(subjectsViewModel);
        }

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

        public IActionResult Create()
        {
            var randomNumber = new Random();

            Subject subject = new Subject()
            {
                Name = $"ASP {randomNumber.Next(0, 10)}",
            };

            dbContext.Subjects.Add(subject);
            dbContext.SaveChanges();

            return RedirectToAction("index");
        }

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
