using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class TypesOfCourse
    {
        public TypesOfCourse()
        {
            Courses = new HashSet<Course>();
        }

        public int TypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
