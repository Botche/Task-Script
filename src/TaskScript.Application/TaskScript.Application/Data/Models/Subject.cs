namespace TaskScript.Application.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Subject
    {
        public Subject()
        {
            this.Lessons = new HashSet<Lesson>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
