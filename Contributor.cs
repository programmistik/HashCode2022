using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2022
{
    public class Contributor
    {
        public string  Name { get; set; }
        public List<Skill> Skills { get; set; }
    }
    public class Skill
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
