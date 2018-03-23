using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public enum RoleNames
    {
        SUPERVISOR, LECTURER, DEMONSTRATOR, SUPERUSER /*, COURSEDIRECTOR*/
    }
    public class Role
    {
        public int RoleID { get; set; }
        public RoleNames RoleType { get; set; }
        public int UserID { get; set; }
    }
}
