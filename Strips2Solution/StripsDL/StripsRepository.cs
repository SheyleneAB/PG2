using StripsBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StripsDL
{
    public class StripsRepository
    {
        public List<Strip> LeesStrips(string fileName)
        {
            try
            {
                List<Strip> strips = new List<Strip>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                       
                        string[] parts = line.Split(';');
                        if (parts.Length != 5)
                        {
                            throw new Exception($"Invalid format in line: {line}");
                        }

                        int reeksNummer = int.Parse(parts[0]);
                        string titel = parts[1];
                        string uitgeverijNaam = parts[2];
                        string reeksNaam = parts[3];
                        string auteursString = parts[4];

                        List<Auteur> auteurs = auteursString
                                                            .Split('|')
                                                            .Select(a => new Auteur(a.Trim(), null))
                                                            .ToList();
                        Strip strip = new Strip(titel, auteurs,  new Reeks(reeksNaam, reeksNummer), new Uitgeverij(uitgeverijNaam));
                        strips.Add(strip);
                    }
                }
                return strips;
            }
            catch (Exception ex)
            {
                throw new Exception($"FileProcessor-LeesStrips [{fileName}]", ex);
            }

        }
    }
}
