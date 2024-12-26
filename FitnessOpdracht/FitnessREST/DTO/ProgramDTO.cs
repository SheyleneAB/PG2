namespace FitnessREST.DTO
{
    public class ProgramDTO
    {
        public ProgramDTO(string name, string target, DateTime startDate, int maxMembers)
        {
            Name = name;
            Target = target;
            StartDate = startDate;
            MaxMembers = maxMembers;
        }

        public string Name { get; set; }
        public string Target { get; set; }
        public DateTime StartDate { get; set; }
        public int MaxMembers { get; set; }

    }
}
