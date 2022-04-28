using System;
using System.Collections.Generic;

namespace AlumniSite.Models
{
    public partial class Address
    {
        public Address()
        {
            AlumniUsers = new HashSet<AlumniUser>();
            Employers = new HashSet<Employer>();
        }

        public int AddressId { get; set; }
        public string? Address1 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<AlumniUser> AlumniUsers { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
    }
}
