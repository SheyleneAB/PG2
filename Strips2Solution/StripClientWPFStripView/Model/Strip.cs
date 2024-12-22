using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace StripsBL.Model
{
    public class Strip
    {
        public int? Id { get; set; }
        public string Titel { get; set ; }
        public Reeks Reeks { get; set; }
        public Uitgeverij Uitgeverij { get; set; }
        public List<Auteur> Auteurs { get; set; }
        public override string ToString()
        {
            return $"{Id},{Titel},\n\t{string.Join("\n\t", Reeks)}, \n\t{string.Join("\n\t",Uitgeverij)}";
        }
    }
}
