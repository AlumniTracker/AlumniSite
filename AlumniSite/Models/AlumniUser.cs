using System;
using System.Collections.Generic;

namespace AlumniSite.Models
{
    public partial class AlumniUser
    {
        public AlumniUser()
        {
            Employers = new HashSet<Employer>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string UserPw { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public int AddressId { get; set; }
        public int? EmployerId { get; set; }
        public string? EmployerName { get; set; }
        public string? YearGraduated { get; set; }
        public string Degree { get; set; } = null!;
        public string? Notes { get; set; }
        public string? AdminType { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Employer? Employer { get; set; }

        public virtual ICollection<Employer> Employers { get; set; }
    }
}
