using FitnessDomein.Model;

namespace FitnessDomein.Interfaces
{
    public interface ITimeSlotRepositoryEF
    {
        Timeslot GeefTimeSlot(int timeSlotId);
    }
}