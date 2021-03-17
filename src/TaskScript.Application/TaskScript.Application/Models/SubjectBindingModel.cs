namespace TaskScript.Application.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class SubjectBindingModel
    {
        [DisplayName("Subject name")]
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
