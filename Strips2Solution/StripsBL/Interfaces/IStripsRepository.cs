using StripsBL.Model;

namespace StripsBL.Interfaces
{
    public interface IStripsRepository
    {
        Strip GeefStrip(int id);
    }
}