namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class TimeSlotServiceException : Exception
    {
        public TimeSlotServiceException()
        {
        }

        public TimeSlotServiceException(string? message) : base(message)
        {
        }

        public TimeSlotServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}