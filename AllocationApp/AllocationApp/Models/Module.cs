using System.Collections.Generic;
using System.ComponentModel;

namespace AllocationApp.Models
{
    // module would be a better name for this class, but to avoid confusion with .net names, I named it Course
    public class Module
    {
        public int ModuleID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        //public User Director { get; set; }
        public List<ModuleUser> Users { get; set; }
        //public List<User> Demonstrators { get; set; }
        [DisplayName("Skills Required")]
        public List<Skill> SkillRequirements { get; set; }
    }
}