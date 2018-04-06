using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AllocationApp.Data.Entities
{
    public class AppUser : IdentityUser
    { 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static implicit operator Task<object>(AppUser v)
        {
            throw new NotImplementedException();
        }
    }
}
