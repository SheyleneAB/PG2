using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Uitgeverij
    {
        private int? id;
        private string adres;

        public Uitgeverij(string naam)
        {
            Naam = naam;
        }

        public Uitgeverij(int uitgeverijid, string naam, string adres)
        {
            Id = uitgeverijid;
            Naam = naam;
            this.adres = adres;
        }

        public string Naam {  get; set; }
        public int? Id { get; set; }

    }
}
