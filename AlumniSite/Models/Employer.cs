using System;
using System.Collections.Generic;

namespace AlumniSite.Models
{
    public partial class Employer
    {
        public Employer()
        {
            AlumniUsers = new HashSet<AlumniUser>();
            Users = new HashSet<AlumniUser>();
        }

        public int EmployerId { get; set; }
        public int AddressId { get; set; }
        public string? EmployerName { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual ICollection<AlumniUser> AlumniUsers { get; set; }

        public virtual ICollection<AlumniUser> Users { get; set; }
    }
}
