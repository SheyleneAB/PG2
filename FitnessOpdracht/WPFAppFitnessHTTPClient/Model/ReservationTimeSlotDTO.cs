namespace WPFAppFitnessHTTPClient.Model
{
    public class ReservationTimeSlotDTO
    {
        public ReservationTimeSlotDTO() { }

        public ReservationTimeSlotDTO(int equipmentId, int timeSlotId)
        {
            EquipmentId = equipmentId;
            TimeSlotId = timeSlotId;
        }

        public int EquipmentId { get; set; }
        public int TimeSlotId { get; set; }
    }
}
