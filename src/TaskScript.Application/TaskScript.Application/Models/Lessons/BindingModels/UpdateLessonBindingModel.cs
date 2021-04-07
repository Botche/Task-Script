namespace TaskScript.Application.Models.Lessons.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UpdateLessonBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Duration")]
        [Range(1, 24)]
        public double Hours { get; set; }

        [Required]
        [DisplayName("Is online")]
        public bool IsOnline { get; set; }

        [DisplayName("Presentation date")]
        public DateTime? PresentationDate { get; set; }

        [Range(0, 64)]
        public int? Seats { get; set; }

        [Required]
        [DisplayName("Subject name")]
        public int SubjectId { get; set; }
    }
}
