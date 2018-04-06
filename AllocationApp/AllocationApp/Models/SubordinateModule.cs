using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class SubordinateModule
    {
        public int SubordinateID { get; set; }
        public int ModuleID { get; set; }

        public virtual User Users { get; set; }
        public virtual Module Module { get; set; }
    }
}
