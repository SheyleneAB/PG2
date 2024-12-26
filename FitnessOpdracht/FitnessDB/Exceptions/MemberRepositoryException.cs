namespace FitnessDB.Exceptions
{
    [Serializable]
    internal class MemberRepositoryException : Exception
    {
        public MemberRepositoryException()
        {
        }

        public MemberRepositoryException(string? message) : base(message)
        {
        }

        public MemberRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}