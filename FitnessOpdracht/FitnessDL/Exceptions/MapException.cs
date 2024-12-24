namespace FitnessDL.Exceptions
{
    [Serializable]
    internal class MapException : Exception
    {
        public MapException()
        {
        }

        public MapException(string? message) : base(message)
        {
        }

        public MapException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}