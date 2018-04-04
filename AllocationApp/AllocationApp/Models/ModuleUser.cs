using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class ModuleUser
    {
        public ModuleUser()
        {
        }
        public ModuleUser(int userID, User user, int moduleID, Module module, double allocatedHours = 6, double hoursWorked = 0)
        {
            UserID = userID;
            this.User = user;
            ModuleID = moduleID;
            this.Module = module;
            HoursAllocated = allocatedHours;
            HoursWorked = hoursWorked;
        }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int ModuleID { get; set; }
        public virtual Module Module { get; set; }
        public double HoursAllocated { get; set; }
        public double HoursWorked { get; set; }
    }
}
