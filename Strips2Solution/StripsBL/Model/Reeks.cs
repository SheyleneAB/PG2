using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Reeks
    {
        public Reeks(){}
        public Reeks(string naam, int? reeksnummer)
        {
            Naam = naam;
        }

        public Reeks(string naam, int? id, int? reeksnummer)
        {
            Naam = naam;
            Id = id;
        }

        public string Naam { get; set; }
        public int? Id { get; set; }
       
    }
}
