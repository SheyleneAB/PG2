namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class DomeinException : Exception
    {
        public DomeinException()
        {
        }

        public DomeinException(string? message) : base(message)
        {
        }

        public DomeinException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}