using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class UserRole
    {
        public UserRole(int userID, User user, int roleID, Role role)
        {
            UserID = userID;
            this.User = user;
            RoleID = roleID;
            this.Role = role;
        }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}
