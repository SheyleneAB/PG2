namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class TrainingsServiceException : Exception
    {
        public TrainingsServiceException()
        {
        }

        public TrainingsServiceException(string? message) : base(message)
        {
        }

        public TrainingsServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}