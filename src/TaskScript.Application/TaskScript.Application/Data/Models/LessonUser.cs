namespace TaskScript.Application.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LessonUser
    {
        public int Id { get; set; }

        [Required]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
