namespace WPFAppFitnessHTTPClient.Model
{
    internal class ReservationTimeSlot
    {
        public Equipment Equipment { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public override string ToString()
        {
            return $" \n\t{string.Join("\n\t", Equipment)}, \n\t{string.Join("\n\t",TimeSlot)}";
        }
    }
}