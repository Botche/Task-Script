namespace TaskScript.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskScript.Application.Models.Lessons.BindingModels;
    using TaskScript.Application.Models.Lessons.ViewModels;

    public interface ILessonsService
    {
        IEnumerable<GetAllLessonsViewModel> GetAll();

        LessonViewModel GetById(int id);

        UpdateLessonBindingModel GetByIdForUpdateMethod(int id);

        Task<int> CreateAsync(CreateLessonBindingModel model);

        Task<bool> UpdateAsync(UpdateLessonBindingModel model);

        Task<bool> DeleteAsync(int id);
    }
}
