﻿using StripsBL.Model;

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
        bool HeeftStripId(int stripid);
        public bool HeeftReeks(Reeks reeks);
        public bool HeeftUitgeverij(Uitgeverij uitgeverij);
        bool HeeftUitgeverijId(int id);
        bool HeeftAuteurId(int id);
        public int VoegAuteurtoe(Auteur auteur);
        public int VoegReeksToe(Reeks reeks);
        public void UpdateStripTitel(Strip strip);
        public void UpdateUitgeverijgeg(Uitgeverij uitgeverij);
        public void UpdateAuteurgeg(Auteur auteur);
        public void VeranderReeks(Strip strip, Reeks reeks);
        public void VeranderUitgever(Strip strip, Uitgeverij uitgeverij);
        int AuteurGetId(Auteur auteur);
        int VoegUitgeverijToe(Uitgeverij uitgever);
        bool HeeftReeksId(int id);
    }
}