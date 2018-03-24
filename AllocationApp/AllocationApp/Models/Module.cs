using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Module
    {
        public int ModuleID { get; set; }
        public String Name { get; set; }

        public virtual ICollection<SubordinateModule> SubordinateModules { get; set; }
    }
}
