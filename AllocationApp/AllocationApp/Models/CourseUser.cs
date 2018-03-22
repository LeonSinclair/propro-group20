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
        public int UserID { get; set; }
        public String UserName { get; set; }
<<<<<<< HEAD:AllocationApp/AllocationApp/Models/CourseWork.cs
        
        public int CourseId { get; set; }
=======
        public int CourseID { get; set; }
>>>>>>> 0ab943c0113655112be6d5e10ed4421b7f39fe51:AllocationApp/AllocationApp/Models/CourseUser.cs
        public String CourseName { get; set; } 
    }
}
