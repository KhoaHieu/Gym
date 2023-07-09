using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int? StaffId { get; set; }
        public string? AccountName { get; set; }
        public string? Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Active { get; set; }

        public virtual Staff? Staff { get; set; }
    }
}
