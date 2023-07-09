using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Contract
    {
        public Contract()
        {
            Bookings = new HashSet<Booking>();
        }
        public int ContractId { get; set; }
        public int? CustomerId { get; set; }
        public int? CourseId { get; set; }
        public int? StaffId { get; set; }
        public int? DaysLeft { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
