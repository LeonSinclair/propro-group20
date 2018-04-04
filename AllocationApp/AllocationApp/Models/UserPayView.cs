using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class UserPayView
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public double UnPaidSum { get; set; }
    }
}
