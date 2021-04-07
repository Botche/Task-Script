namespace TaskScript.Application.Models.Subjects.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class SubjectsViewModel
    {
        public IEnumerable<IdNameViewModel> Subjects { get; set; }

        public string Username { get; set; }

        public DateTime TimeNow { get; set; }
    }
}
