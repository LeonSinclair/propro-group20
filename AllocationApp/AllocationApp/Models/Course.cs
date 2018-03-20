using System.Collections.Generic;

namespace AllocationApp.Models
{
    // module would be a better name for this class, but to avoid confusion with .net names, I named it Course
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        //public User Director { get; set; }
        public List<CourseWork> Lecturers { get; set; }
        //public List<User> Demonstrators { get; set; }
        public List<Skill> Requirements { get; set; }
        

    }
}