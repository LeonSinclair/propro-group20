using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class CourseUser
    {
        public CourseUser()
        {
        }
        public CourseUser(int userID, User user, int courseID, Course course)
        {
            UserID = userID;
            this.User = user;
            CourseID = courseID;
            this.Course = course;
        }

        public int UserID { get; set; }
        public User User { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; } 
    }
}
