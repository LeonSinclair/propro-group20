using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Hour
    {
        public int HourID { get; set; }
        //who did the work?
        public int UserID { get; set; }
        //what type of work did the user do?
        public String HourType { get; set; }
        //which course did he work for?
        public int ModuleID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //type needs to be changed to an exact money type
        public int PayRate { get; set; }
        public Boolean IsApproved { get; set; }
        
    }
}
