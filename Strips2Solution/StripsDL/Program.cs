using StripsBL.Model;
using StripsDL;

internal class Program
{
    private static void Main(string[] args)
    {
        FileProcessor FP = new FileProcessor();
        string filestring = @"C:\Users\elyne\Downloads\stripsData (2).txt";
        List<Strip> Strips =  FP.LeesStrips(filestring);
        foreach (Strip strip in Strips) {
            Console.WriteLine(strip.Reeksnummer.ToString(), strip.Id);

        }
        string connectionstring = "Data Source=Radion\\sqlexpress;Initial Catalog=Strips;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        StripsRepository repo = new StripsRepository(connectionstring);
        Strip destrip = repo.GeefStrip(1);
        Console.WriteLine(destrip.Titel + destrip.Reeks.Naam + destrip.Uitgeverij.Naam);
        

    }
}