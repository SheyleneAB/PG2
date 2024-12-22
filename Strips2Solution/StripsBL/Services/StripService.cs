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

        public void VerwijderAuteur(int stripId, int auteurId)
        {
            repo.RemoveAuteurFromStrip(stripId, auteurId);
        }

        public void VoegAuteurToe(int stripId, Auteur auteur)
        {
            //TODO: check if auteur WHAT is going wrong here
            repo.AddAuteurToStrip(stripId, auteur);
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
