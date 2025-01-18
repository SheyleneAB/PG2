namespace WPFAppFitnessHTTPClient.Model
{
    internal class ReservationTimeSlot
    {
        public ReservationTimeSlot()
        {

        }
        public ReservationTimeSlot(Equipment equipment, TimeSlot timeSlot)
        {
            Equipment = equipment;
            TimeSlot = timeSlot;
        }

        public Equipment Equipment { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public override string ToString()
        {
            return $" \n\t{string.Join("\n\t", Equipment)}, \n\t{string.Join("\n\t",TimeSlot)}";
        }
    }
}