using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripClientWPFStripView.Model
{
    public class Uitgeverij
    {
        public string? Adres { get; set; }
        public string Naam { get; set; }
        public int? Id { get; set; }

        public override string ToString()
        {
            return $"{Naam}, {Id}, {Adres}";
        }

    }
}
