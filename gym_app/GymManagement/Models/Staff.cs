using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Accounts = new HashSet<Account>();
            Bookings = new HashSet<Booking>();
            Contracts = new HashSet<Contract>();
        }

        public int StaffId { get; set; }
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public int? RoleId { get; set; }
        public bool Active { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
