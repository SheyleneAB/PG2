using FitnessDomein.Model;

namespace FitnessREST.DTO
{
    public class ReservationDTO
    {
        public ReservationDTO() { }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public List <ReservationTimeSlotDTO> ReservationTimeSlot { get; set; }
    }
}