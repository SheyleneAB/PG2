using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Reeks
    {
        public Reeks(string naam,  int reeksnummer)
        {
            Naam = naam;
            Reeksnummer = reeksnummer;
        }

        public string Naam { get; set; }
        public int? Id { get; set; }
        public int Reeksnummer { get; set; }
    }
}
