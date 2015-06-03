using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkillsManagerWebApplication.Models
{
    public class Technology
    {
        public int TechnologyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}