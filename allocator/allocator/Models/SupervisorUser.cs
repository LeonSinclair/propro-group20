using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class SupervisorUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
