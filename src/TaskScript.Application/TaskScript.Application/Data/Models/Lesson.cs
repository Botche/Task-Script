﻿namespace TaskScript.Application.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Lesson
    {
        public Lesson()
        {
            this.LessonsUsers = new HashSet<LessonUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [Range(1, 24)]
        public double Hours { get; set; }

        [Required]
        public bool IsOnline { get; set; }

        public DateTime PresentationDate { get; set; }

        [Range(0, 64)]
        public int? Seats { get; set; }

        [Required]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<LessonUser> LessonsUsers { get; set; }
    }
}
