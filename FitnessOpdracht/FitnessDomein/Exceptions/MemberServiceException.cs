namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class MemberServiceException : Exception
    {
        public MemberServiceException()
        {
        }

        public MemberServiceException(string? message) : base(message)
        {
        }

        public MemberServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}