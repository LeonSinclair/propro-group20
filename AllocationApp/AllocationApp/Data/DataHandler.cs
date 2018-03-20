using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllocationApp.Models;

namespace AllocationApp.Data
{
    public class DataHandler
    {
        public static void InsertCourse(Course myCourse)
        {
            using (var context = new AllocationDBContext())
            {
                context.Courses.Add(myCourse);
                context.SaveChanges();
            }
        }
    }
}
