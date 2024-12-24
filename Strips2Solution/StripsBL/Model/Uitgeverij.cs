using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Uitgeverij
    {
        public Uitgeverij() { }
        private int? uitgeverijid;
        private string adres;

        public Uitgeverij(string naam)
        {
            Naam = naam;
        }

        public Uitgeverij(int? uitgeverijid, string naam, string? adres)
        {
            this.uitgeverijid = uitgeverijid;
            Naam = naam;
            Adres = adres;
        }
        public int? UitgeverijId { get; set; }

        public string Naam {  get; set; }
        public string? Adres { get; set; }

    }
}
