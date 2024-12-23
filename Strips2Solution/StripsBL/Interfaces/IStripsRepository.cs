using StripsBL.Model;

namespace StripsBL.Interfaces
{
    public interface IStripsRepository
    {
        Strip GeefStrip(int id);
        void RemoveAuteurFromStrip(int stripId, int auteurId);
        public List<Strip> GeefReeksMStrip(int reeksnummer);
        public void AddAuteurToStrip(int stripId, int auteurid);
        public bool HeeftAuteur(Auteur auteur);
        public bool HeeftStrip(Strip strip);
        public bool HeeftReeks(Reeks reeks);
        public bool HeeftUitgeverij(Uitgeverij uitgeverij);
        public int VoegAuteurtoe(Auteur auteur);
        public void UpdateStripTitel(Strip strip);
        public void UpdateUitgeverijgeg(Uitgeverij uitgeverij);
        public void UpdateAuteurgeg(Auteur auteur);
        public void VeranderReeks(Strip strip, Reeks reeks);
        public void VeranderUitgever(Strip strip, Uitgeverij uitgeverij);
    }
}