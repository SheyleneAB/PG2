using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Program
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public int MaxMembers { get; set; }
        public DateTime StartDate { get; set; }
        public string programCode { get; set; }
        public List<Member> Members { get; set; }

    }
}
