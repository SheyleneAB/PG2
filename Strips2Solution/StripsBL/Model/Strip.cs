using StripsBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Strip
    {
        public int? Id { get; set; }
        private string titel;
        public string Titel { get { return titel; } set { if (string.IsNullOrWhiteSpace(value)) throw new DomeinException("SetTitel"); titel = value; } }

        private List<Auteur> auteurs=new();
        public Reeks Reeks { get; set; }
        public Uitgeverij Uitgeverij { get; set; }

        public Strip(string titel,  Reeks reeks, Uitgeverij uitgeverij)
        {
            Titel = titel;
            Reeks = reeks;
            Uitgeverij = uitgeverij;
        }

        public Strip(string titel, List<Auteur> auteurs, Reeks reeks, Uitgeverij uitgeverij)
        {
            Titel = titel;
            Auteurs = auteurs;
            Reeks = reeks;
            Uitgeverij = uitgeverij;
        }

        public IReadOnlyList<Auteur> Auteurs { 
            get {return auteurs;}
            set { 
                if ((value == null) || (value.Count < 1)) throw new DomeinException("SetAuteurs"); 
                foreach(Auteur auteur in value) { VoegAuteurToe(auteur); }
            }
        }
        public void VoegAuteurToe(Auteur auteur)
        {
            if ((auteur==null) || auteurs.Contains(auteur)) { throw new DomeinException("VoegAuteurToe"); }
            auteurs.Add(auteur);
        }
        public void VerwijderAuteur(Auteur auteur) 
        {
            if ((auteur == null) 
                || !auteurs.Contains(auteur)
                || auteurs.Count<=1) throw new DomeinException("VerwijderAuteur");
            auteurs.Remove(auteur);
        }
    }
}
