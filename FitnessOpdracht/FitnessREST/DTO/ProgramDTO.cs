namespace FitnessREST.DTO
{
    public class ProgramDTO
    {
        public ProgramDTO(string programcode, string name, string target, DateTime startDate, int maxMembers)
        {
            ProgramCode = programcode;
            Name = name;
            Target = target;
            StartDate = startDate;
            MaxMembers = maxMembers;
        }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public DateTime StartDate { get; set; }
        public int MaxMembers { get; set; }

    }
}
