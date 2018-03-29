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
        public Proposal(int userID, User user, int moduleID, Module module, bool approved)
        {
            UserID = userID;
            this.User = user;
            ModuleID = moduleID;
            this.Module = module;
            this.Approved = approved;
        }

        public int ModuleID { get; set; }
        public virtual Module Module { get; set; } 
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public bool Approved { get; set; }
    }
}
