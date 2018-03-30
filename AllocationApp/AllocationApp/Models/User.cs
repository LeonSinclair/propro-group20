using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllocationApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Roles")]
        public List<Role> Roles { get; set; }

        public List<CourseUser> Courses { get; set; }

        public List<Hour> WorkHours { get; set; }

        public List<Skill> Skills { get; set; }

        // Bank Details
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }

        [DisplayName("IBAN")]
        public string IBAN { get; set; }

        [DisplayName("Sort Code")]
        public int SortCode { get; set; }
    }
}
