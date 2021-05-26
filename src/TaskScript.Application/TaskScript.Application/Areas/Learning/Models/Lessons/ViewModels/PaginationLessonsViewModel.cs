namespace TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels
{
    using System.Collections.Generic;

    public class PaginationLessonsViewModel
    {
        public IEnumerable<GetAllLessonsViewModel> Lessons { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
