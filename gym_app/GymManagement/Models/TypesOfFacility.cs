using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class TypesOfFacility
    {
        public TypesOfFacility()
        {
            Facilities = new HashSet<Facility>();
        }

        public int TypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
