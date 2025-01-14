namespace WPFAppFitnessHTTPClient.Model
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string PartOfDay { get; set; }
        public override string ToString()
        {
            return $"{Id},{StartTime},{EndTime},{PartOfDay}";
        }
    }
}