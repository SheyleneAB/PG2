using StripsBL.Model;

namespace StripsBL.Interfaces
{
    public interface IStripsRepository
    {
        Strip GeefStrip(int id);
        void RemoveAuteurFromStrip(int stripId, int auteurId);
        void AddAuteurToStrip(int stripId, Auteur auteur);
    }
}