namespace TaskScript.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskScript.Application.Areas.Learning.Models.Subjects.BindingModels;
    using TaskScript.Application.Areas.Learning.Models.Subjects.ViewModels;
    using TaskScript.Application.Data.Models;

    public interface ISubjectsService
    {
        IEnumerable<IdNameViewModel> GetAll();

        Subject GetById(int id);

        SubjectViewModel GetForViewById(int id);

        Subject GetByName(string name);

        Task CreateAsync(SubjectBindingModel model);

        Task UpdateAsync(SubjectUpdateBindingModel model);

        Task RemoveAsync(int id);
    }
}
