namespace WPFAppFitnessHTTPClient.Model
{
    public class Equipment
    {
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public bool IsInMaintenance { get; set; }
        public override string ToString()
        {
            return $"{Id},{DeviceType},{IsInMaintenance}";
        }
    }
}