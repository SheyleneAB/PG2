namespace FitnessDL.Exceptions
{
    [Serializable]
    internal class EquipmentRepositoryException : Exception
    {
        public EquipmentRepositoryException()
        {
        }

        public EquipmentRepositoryException(string? message) : base(message)
        {
        }

        public EquipmentRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}