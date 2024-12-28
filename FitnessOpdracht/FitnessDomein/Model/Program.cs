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

        public Program(string programcode,string name, string target,  DateTime startDate, int maxMembers)
        {
            ProgramCode = programcode;
            Name = name;
            Target = target;
            MaxMembers = maxMembers;
            StartDate = startDate;
           
        }

        public string Name { get; set; }
        public string Target { get; set; }
        public int MaxMembers { get; set; }
        public DateTime StartDate { get; set; }
        public string ProgramCode { get; set; }
        public List<Member> Members { get; set; }

    }
}
