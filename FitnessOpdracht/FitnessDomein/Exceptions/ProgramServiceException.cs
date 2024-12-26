namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class ProgramServiceException : Exception
    {
        public ProgramServiceException()
        {
        }

        public ProgramServiceException(string? message) : base(message)
        {
        }

        public ProgramServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}