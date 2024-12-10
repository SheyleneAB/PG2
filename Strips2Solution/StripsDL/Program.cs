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
            Console.WriteLine(strip.Titel, strip.Id);

        }
       
    }
}