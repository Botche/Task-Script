namespace TaskScript.Application.Areas.Learning.Models.Lessons.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateLessonBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Name { get; set; }

        [DisplayName("Duration")]
        [Required]
        [Range(1, 24)]
        public double Hours { get; set; }

        [DisplayName("Is online")]
        [Required]
        public bool IsOnline { get; set; }

        [DisplayName("Presentation date")]
        public DateTime? PresentationDate { get; set; }

        [Range(0, 64)]
        public int? Seats { get; set; }

        [DisplayName("Subject name")]
        [Required]
        public int SubjectId { get; set; }
    }
}
