using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class AddUserToRoleView
    {
        public List<Role> Roles { get; set; }
        public int UserID { get; set; }
    }
}
