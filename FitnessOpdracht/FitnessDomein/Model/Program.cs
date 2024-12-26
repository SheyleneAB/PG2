using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Program
    {
        public Program() { }

        public Program(string name, string target,  DateTime startDate, int maxMembers)
        {
            Name = name;
            Target = target;
            MaxMembers = maxMembers;
            StartDate = startDate;
           
        }

        public string Name { get; set; }
        public string Target { get; set; }
        public int MaxMembers { get; set; }
        public DateTime StartDate { get; set; }
        public string? programCode { get; set; }
        public List<Member> Members { get; set; }

    }
}
