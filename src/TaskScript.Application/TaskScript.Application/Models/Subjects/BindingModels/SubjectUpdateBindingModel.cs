namespace TaskScript.Application.Models.Subjects.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class SubjectUpdateBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
