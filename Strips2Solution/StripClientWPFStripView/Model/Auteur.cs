using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace StripClientWPFStripView.Model
{
    public class Auteur
    {

        public string Naam { get; set; }
        public string? Email { get; set; }
        public int? Id { get; set; }

        public override string ToString()
        {
            return $"{Naam}, {Email}, {Id}";
        }



    }
}
