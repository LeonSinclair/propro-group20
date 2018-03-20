using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Hour
    {
        public int Id { get; set; }
        //who did the work?
        public User UserId { get; set; }
        //what type of work did the user do?
        public int HourType { get; set; }
        //which course did he work for?
        public Course CourseId { get; set; }
        //type of date needs to be changed later to a more exact date time
        public String Date { get; set; }
        //type needs to be changed to an exact money type
        public int PayRate { get; set; }
        public Boolean IsApproved { get; set; }
        
    }
}
