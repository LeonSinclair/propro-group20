using System;
using System.Collections.Generic;
using System.Text;

namespace allocator.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Role> RoleType { get; set; }
        public List<Course> Courses { get; set; }
        public List<Hour> WorkHours { get; set; }
        public List<Skill> Skills { get; set; }
        //todo payslip
    }
}
