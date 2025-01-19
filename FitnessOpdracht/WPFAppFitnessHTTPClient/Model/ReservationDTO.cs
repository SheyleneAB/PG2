namespace WPFAppFitnessHTTPClient.Model
{
    public class ReservationDTO
    {
        public ReservationDTO() { }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public List<ReservationTimeSlotDTO> Reservationtimeslot { get; set; }
    }
}