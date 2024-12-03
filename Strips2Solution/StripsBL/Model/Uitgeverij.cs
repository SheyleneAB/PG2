using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Uitgeverij
    {
        public Uitgeverij(string naam)
        {
            Naam = naam;
        }

        public string Naam {  get; set; }

    }
}
