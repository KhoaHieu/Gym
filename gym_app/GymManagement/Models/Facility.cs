using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Facility
    {
        public int FacilityId { get; set; }
        public string? Name { get; set; }
        public int? TypeId { get; set; }
        public int? Quantity { get; set; }
        public int? PricePerUnit { get; set; }
        public string? Description { get; set; }

        public virtual TypesOfFacility? Type { get; set; }
    }
}
