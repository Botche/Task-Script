namespace TaskScript.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskScript.Application.Data;
    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Models.Subjects.BindingModels;
    using TaskScript.Application.Models.Subjects.ViewModels;
    using TaskScript.Application.Services.Interfaces;

    public class SubjectsService : ISubjectsService
    {
        private readonly ApplicationDbContext dbContext;

        public SubjectsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<IdNameViewModel> GetAll()
        {
            IEnumerable<IdNameViewModel> subjects = this.dbContext.Subjects
                .Select(subject => new IdNameViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                })
                .OrderBy(subject => subject.Name)
                .ToList();

            return subjects;
        }

        public Subject GetById(int id)
        {
            Subject subject = this.dbContext.Subjects
                .Where(subject => subject.Id == id)
                .SingleOrDefault();
            //.SingleOrDefault(subject => subject.Id == id);

            return subject;
        }

        public SubjectViewModel GetForViewById(int id)
        {
            SubjectViewModel subject = this.dbContext.Subjects
                .Select(s => new SubjectViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .Where(s => s.Id == id)
                .SingleOrDefault();

            return subject;
        }

        public Subject GetByName(string name)
        {
            Subject subjectFromDb = this.dbContext.Subjects
                .Where(s => s.Name == name)
                .SingleOrDefault();

            return subjectFromDb;
        }

        public async Task CreateAsync(SubjectBindingModel model)
        {
            Subject subject = new Subject();
            subject.Name = model.Name;

            await this.dbContext.Subjects.AddAsync(subject);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubjectUpdateBindingModel model)
        {
            Subject subject = this.GetById(model.Id);

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return;
            }

            subject.Name = model.Name;

            this.dbContext.Subjects.Update(subject);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Subject subject = this.GetById(id);

            bool isSubjectNull = subject == null;
            if (isSubjectNull)
            {
                return;
            }

            this.dbContext.Subjects.Remove(subject);
            await this.dbContext.SaveChangesAsync();
        }

        //public IEnumerable<SubjectViewModel> GetAll()
        //{
        //    List<SubjectViewModel> subjects = this.dbContext.Subjects
        //        .Select(subject => new SubjectViewModel
        //        {
        //            Id = subject.Id,
        //            Name = subject.Name,
        //        })
        //        //.Where(subject => subject.Name.Contains("ASP"))
        //        .OrderBy(subject => subject.Name)
        //        //.Skip(2)
        //        //.Take(2)
        //        .ToList();

        //    return subjects;
        //}
    }
}
