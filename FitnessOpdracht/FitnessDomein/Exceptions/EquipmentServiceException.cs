namespace FitnessDomein.Exceptions
{
    [Serializable]
    internal class EquipmentServiceException : Exception
    {
        public EquipmentServiceException()
        {
        }

        public EquipmentServiceException(string? message) : base(message)
        {
        }

        public EquipmentServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}