using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllocationApp.Models;

namespace AllocationApp.Data
{
    public class DataHandler
    {
        public static void InsertCourse(Module myModule)
        {
            using (var context = new AllocationContext())
            {
                context.Modules.Add(myModule);
                context.SaveChanges();
            }
        }
    }
}
