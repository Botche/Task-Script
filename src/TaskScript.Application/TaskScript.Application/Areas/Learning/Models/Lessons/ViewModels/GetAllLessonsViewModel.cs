namespace TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels
{
    public class GetAllLessonsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool CurrentUserIsEnrolled { get; set; }
    }
}
