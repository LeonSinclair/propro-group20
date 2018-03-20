using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class CourseWork
    {
        public int UserId { get; set; }
        public String UserName { get; set; }

        [Key]
        public int CourseId { get; set; }
        public String CourseName { get; set; } 
    }
}
