namespace StripsBL.Exceptions
{
    [Serializable]
    internal class StripServiceException : Exception
    {
        public StripServiceException()
        {
        }

        public StripServiceException(string? message) : base(message)
        {
        }

        public StripServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}