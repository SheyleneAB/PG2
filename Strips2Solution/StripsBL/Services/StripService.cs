using StripsBL.Exceptions;
using StripsBL.Interfaces;
using StripsBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Services
{
    public class StripService
    {
        private IStripsRepository repo;

        public StripService(IStripsRepository repo)
        {
            this.repo = repo;
        }

        public void VerwijderAuteur(int stripId, Auteur auteur)
        {
            try
            { 
                if (!repo.HeeftAuteur(auteur)) throw new StripServiceException("VerwijderAuteur - Auteur bestaat niet");
                else
                {
                    auteur.Id = repo.AuteurGetId(auteur);
                    repo.RemoveAuteurFromStrip(stripId, auteur.Id.Value);
                }

            }
            catch (Exception ex)
            {
                throw new StripServiceException("VerwijderAuteur", ex);
            }

        }
        public Auteur VoegAuteurToe(int stripId, Auteur auteur)
        {
            try
            {
                if (auteur == null) throw new StripServiceException("VoegAuteurToe - auteur is null");
                if (!repo.HeeftAuteur(auteur))
                {
                    auteur.Id = repo.VoegAuteurtoe(auteur);
                }
                repo.AddAuteurToStrip(stripId, auteur.Id.Value);
                return auteur;
            }
            catch (Exception ex)
            {
                throw new StripServiceException("VoegAuteurToe", ex);
            }
        }
        public Strip GeefStrip(int id)
        {
            try
            {
                return repo.GeefStrip(id);
            }
            catch (Exception ex) { throw new StripServiceException("GeefStrip", ex); }
        }
        public List<Strip> GeefReeks(int reeksid)
        {
            try
            {
                return repo.GeefReeksMStrip(reeksid);
            }
            catch (Exception ex) { throw new StripServiceException("GeefReeks", ex); }

        }
        public bool HeeftStrip(int stripid)
        {
            try
            {
                return repo.HeeftStripId(stripid);
            }
            catch (Exception ex) { throw new StripServiceException("HeeftStrip", ex); }
        }
        public bool HeeftUitgeverij(Uitgeverij uitgeverij)
        {
            try
            {
                return repo.HeeftUitgeverij(uitgeverij);
            }
            catch (Exception ex) { throw new StripServiceException("HeeftUitgeverij", ex); }
        }
        public void UpdateStripTitel(Strip strip)
        {
            try
            {
                if (strip == null) throw new StripServiceException("UpdateStripTitel - strip is null");
                if (!repo.HeeftStripId(strip.Id.Value)) throw new StripServiceException("UpdateStripTitel - strip bestaat niet");
                repo.UpdateStripTitel(strip);
            }
            catch (Exception ex) { throw new StripServiceException("UpdateStripTitel", ex); }
        }
        public void UpdateUitgeverijgeg(Uitgeverij uitgeverij)
        {
            try
            {
                if (uitgeverij == null) throw new StripServiceException("UpdateUitgeverijgeg - uitgeverij is null");
                if (!repo.HeeftUitgeverijId(uitgeverij.UitgeverijId.Value)) throw new StripServiceException("UpdateUitgeverijgeg - uitgeverij bestaat niet");
                repo.UpdateUitgeverijgeg(uitgeverij);
            }
            catch (Exception ex) { throw new StripServiceException("UpdateUitgeverijgeg", ex); }
        }
        public void UpdateAuteurgeg(Auteur auteur)
        {
            try
            {
                if (auteur == null) throw new StripServiceException("UpdateAuteurgeg - auteur is null");
                if (!repo.HeeftAuteurId(auteur.Id.Value)) throw new StripServiceException("UpdateAuteurgeg - auteur bestaat niet");
                repo.UpdateAuteurgeg(auteur);
            }
            catch (Exception ex) { throw new StripServiceException("UpdateAuteurgeg", ex); }
        }
        public void VeranderReeks(Strip strip, Reeks reeks)
        {
            try
            {
                if (strip == null) throw new StripServiceException("VeranderReeks - strip is null");
                if (reeks == null) throw new StripServiceException("VeranderReeks - reeks is null");
                if (!repo.HeeftStrip(strip)) throw new StripServiceException("VeranderReeks - strip bestaat niet");
                if (!repo.HeeftReeks(reeks)) reeks.Id = repo.VoegReeksToe(reeks);
                repo.VeranderReeks(strip, reeks);
            }
            catch (Exception ex) { throw new StripServiceException("VeranderReeks", ex); }
        }
        public void VeranderUitgever(Strip strip, Uitgeverij uitgever)
        {
            try
            {
                if (strip == null) throw new StripServiceException("VeranderUitgever - strip is null");
                if (uitgever == null) throw new StripServiceException("VeranderUitgever - uitgever is null");
                if (!repo.HeeftStrip(strip)) throw new StripServiceException("VeranderUitgever - strip bestaat niet");
                if (!repo.HeeftUitgeverij(uitgever)) uitgever.UitgeverijId = repo.VoegUitgeverijToe(uitgever);
                repo.VeranderUitgever(strip, uitgever);
            }
            catch (Exception ex) { throw new StripServiceException("VeranderUitgever", ex); }
        }

        public bool HeeftUitgeverijId(int id)
        {
            try
            {
                return repo.HeeftUitgeverijId(id);
            }
            catch (Exception ex) { throw new StripServiceException("HeeftUitgeverij", ex); }
        }

        public bool HeeftAuteurId(int id)
        {
            try
            {
                return repo.HeeftAuteurId(id); 
            }
            catch (Exception ex) { throw new StripServiceException("HeeftAuteurId", ex); }
        }

        public bool HeeftReeksId(int id)
        {
            try
            {
                return repo.HeeftReeksId(id);
            }
            catch (Exception ex) { throw new StripServiceException("HeeftReeksId", ex); }
        }
    }
}
