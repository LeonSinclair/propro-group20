using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Proposal
    {
        
        public Proposal()
        {

        }
        public Proposal(int userID, User user, int courseID, Course course, bool approved)
        {
            UserID = userID;
            this.User = user;
            CourseID = courseID;
            this.Course = course;
            this.Approved = approved;
        }

        public int CourseID { get; set; }
        public Course Course { get; set; } 
        public int UserID { get; set; }
        public User User { get; set; }
        public bool Approved { get; set; }
    }
}
