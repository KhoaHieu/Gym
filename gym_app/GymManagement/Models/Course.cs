using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Course
    {
        public Course()
        {
            Contracts = new HashSet<Contract>();
        }

        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int? Type { get; set; }
        public int? Duration { get; set; }
        public int? NumberOfSession { get; set; }
        public bool Active { get; set; }

        public virtual TypesOfCourse? TypeNavigation { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
