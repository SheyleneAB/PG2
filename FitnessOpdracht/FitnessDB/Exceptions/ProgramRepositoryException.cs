namespace FitnessDB.Exceptions
{
    [Serializable]
    internal class ProgramRepositoryException : Exception
    {
        public ProgramRepositoryException()
        {
        }

        public ProgramRepositoryException(string? message) : base(message)
        {
        }

        public ProgramRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}