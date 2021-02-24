namespace TaskScript.Database.Models.Subjects
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using TaskScript.Database.Models.Tasks;

    public class Subject
    {
        public Subject()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        [DisplayName("Name of subject")]
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
