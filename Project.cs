using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2022
{
    public class Project
    {
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
        public int Score { get; set; }
        public int BestBefore { get; set; }
        public int NumberOfRoles { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class PlanProject
    {
        public Project project { get; set; }
        public HashSet<Contributor> contributors { get; set; }
    }
}
