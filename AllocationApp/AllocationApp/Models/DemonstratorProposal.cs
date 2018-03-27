using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class DemonstratorProposal
    {
        public int CourseID { get; set; }
        public Course Course { get; set; } 
        public int UserID { get; set; }
        public User User { get; set; }
        public bool Approved { get; set; }
    }
}
