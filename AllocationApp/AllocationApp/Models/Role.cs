using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public String RoleType { get; set; }
        public List<UserRole> Users { get; set; }
    }
}
