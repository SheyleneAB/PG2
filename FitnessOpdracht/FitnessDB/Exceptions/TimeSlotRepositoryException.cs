namespace FitnessDB.Exceptions
{
    [Serializable]
    internal class TimeSlotRepositoryException : Exception
    {
        public TimeSlotRepositoryException()
        {
        }

        public TimeSlotRepositoryException(string? message) : base(message)
        {
        }

        public TimeSlotRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}