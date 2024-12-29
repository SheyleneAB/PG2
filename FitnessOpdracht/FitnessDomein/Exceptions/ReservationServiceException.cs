namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class ReservationServiceException : Exception
    {
        public ReservationServiceException()
        {
        }

        public ReservationServiceException(string? message) : base(message)
        {
        }

        public ReservationServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}