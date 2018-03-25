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

        public List<Role> RoleType { get; set; }
        public List<CourseUser> Courses { get; set; }
        public List<Hour> WorkHours { get; set; }
        public List<Skill> Skills { get; set; }
        //todo payslip
    }
}
