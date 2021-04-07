namespace TaskScript.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskScript.Application.Data.Models;
    using TaskScript.Application.Models.Subjects.BindingModels;
    using TaskScript.Application.Models.Subjects.ViewModels;

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
