namespace TaskScript.Application.Areas.Learning.Models.Lessons.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class LessonViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Duration")]
        public double Hours { get; set; }

        [DisplayName("Is online")]
        public bool IsOnline { get; set; }

        [DisplayName("Presentation date")]
        public DateTime? PresentationDate { get; set; }

        public int? Seats { get; set; }

        [DisplayName("Seats left")]
        public int? SeatsLeft { get; set; }

        [DisplayName("Subject name")]
        public string SubjectName { get; set; }

        public IEnumerable<string> EnrolledUsernames { get; set; }
    }
}
