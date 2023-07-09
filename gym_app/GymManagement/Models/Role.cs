using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Role
    {
        public Role()
        {
            staff = new HashSet<Staff>();
        }

        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Staff> staff { get; set; }
    }
}
