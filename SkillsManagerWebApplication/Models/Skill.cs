using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkillsManagerWebApplication.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public int TechnologyID { get; set; }
        public int EmployeeID { get; set; }
        public string Level { get; set; }

        public virtual Technology Technology { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}