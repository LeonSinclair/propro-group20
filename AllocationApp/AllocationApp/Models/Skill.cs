using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Models
{
    public class Skill
    {
        public Skill(int SkillID, String Name)
        {
            this.SkillID = SkillID;
            this.Name = Name;
        }

        public int SkillID { get; set; }
        public String Name { get; set; }
        //public List<User> Users { get; set; }

    }
}

