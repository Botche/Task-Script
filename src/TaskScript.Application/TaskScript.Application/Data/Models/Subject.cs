﻿namespace TaskScript.Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
