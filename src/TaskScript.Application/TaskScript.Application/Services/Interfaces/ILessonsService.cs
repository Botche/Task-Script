namespace TaskScript.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskScript.Application.Areas.Learning.Models.Lessons.BindingModels;
    using TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels;

    public interface ILessonsService
    {
        PaginationLessonsViewModel GetAll(int page);

        LessonViewModel GetById(int id);

        UpdateLessonBindingModel GetByIdForUpdateMethod(int id);

        bool CheckIfLessonExist(int id);

        Task<int> CreateAsync(CreateLessonBindingModel model);

        Task<bool> UpdateAsync(UpdateLessonBindingModel model);

        Task<bool> DeleteAsync(int id);

        int? GetAllSeats(int lessonId);

        bool CheckIfLessonIsOld(int lessonId);
    }
}
