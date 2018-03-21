using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class CourseUser
    {
        public int UserID { get; set; }
        public String UserName { get; set; }
        public int CourseID { get; set; }
        public String CourseName { get; set; } 
    }
}
