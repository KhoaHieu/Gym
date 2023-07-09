using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Contracts = new HashSet<Contract>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string IdentityNumber { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
