using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class AddUserToModuleView
    {
        public List<Module> Modules { get; set; }
        public User User { get; set; }
    }
}
