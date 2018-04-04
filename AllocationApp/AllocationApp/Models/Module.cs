using System.Collections.Generic;

namespace AllocationApp.Models
{
    public class Module
    {
        public int ModuleID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        //public User Director { get; set; }
        public List<ModuleUser> Users { get; set; }
        //public List<User> Demonstrators { get; set; }
        public List<Skill> SkillRequirements { get; set; }
    }
}