using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AllocationApp.Models
{
    //This class represents the many-to-many relationship between modules ans users (demonstrators)
    public class UserModuleHours
    {
        //primary key
        public int ID { get; set; }
        public UserModuleHours(int userID, User user, int moduleID, Module module, double allocatedHours, double hoursWorked)
        {
            UserID = userID;
            this.User = user;
            ModuleID = moduleID;
            this.Module = module;

        }
        //Foreign Key of User table
        [Required]
        public int UserID { get; set; }
        //Foreign Key of Module table
        [Required]
        public int ModuleID { get; set; }
        public double HoursAllocated { get; set; }
        public double HoursWorked { get; set; }

        public virtual User User { get; set; }
        public virtual Module Module { get; set; }
    }
}
