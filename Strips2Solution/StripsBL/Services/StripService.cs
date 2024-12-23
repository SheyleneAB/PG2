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
                repo.RemoveAuteurFromStrip(stripId, auteur.Id.Value);
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
       
    }
}
