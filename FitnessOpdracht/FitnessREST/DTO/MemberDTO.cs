namespace FitnessREST.DTO
{
    public class MemberDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string? Interests { get; set; }
        public string? MemberType { get; set; }

    }
}