namespace TaskScript.Application.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.UsersLessons = new HashSet<LessonUser>();
        }

        public int? Age { get; set; }

        public virtual ICollection<LessonUser> UsersLessons { get; set; }
    }
}
