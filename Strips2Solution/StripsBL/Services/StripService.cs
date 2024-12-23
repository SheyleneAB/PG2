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
            { //TODO: getauteurid methode schrijven
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
        public void UpdateStripTitel (Strip strip)
        {
            try
            {
                if (strip == null) throw new StripServiceException("UpdateStripTitel - strip is null");
                if (!repo.HeeftStrip(strip)) throw new StripServiceException("UpdateStripTitel - strip bestaat niet");
                repo.UpdateStripTitel(strip);
            }
            catch (Exception ex) { throw new StripServiceException("UpdateStripTitel", ex); }
        }
        public void UpdateUitgeverijgeg(Uitgeverij uitgeverij)
        {
            try
            {
                if (uitgeverij == null) throw new StripServiceException("UpdateUitgeverijgeg - uitgeverij is null");
                if (!repo.HeeftUitgeverij(uitgeverij)) throw new StripServiceException("UpdateUitgeverijgeg - uitgeverij bestaat niet");
                repo.UpdateUitgeverijgeg(uitgeverij);
            }
            catch (Exception ex) { throw new StripServiceException("UpdateUitgeverijgeg", ex); }
        }
        public void UpdateAuteurgeg(Auteur auteur)
        {
            try
            {
                if (auteur == null) throw new StripServiceException("UpdateAuteurgeg - auteur is null");
                if (!repo.HeeftAuteur(auteur)) throw new StripServiceException("UpdateAuteurgeg - auteur bestaat niet");
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
                if (!repo.HeeftReeks(reeks)) throw new StripServiceException("VeranderReeks - reeks bestaat niet");
                repo.VeranderReeks(strip, reeks);
            }
            catch (Exception ex) { throw new StripServiceException("VeranderReeks", ex); }
        }
    }
}
