using FitnessDomein.Model;

namespace FitnessREST.DTO
{
    public class TrainingDTO
    {
        public TrainingDTO() { }
        public List<Cyclingsession> Cyclingsessions { get; set; }
        public List<RunningSession> Runningsessions { get; set; }
    }
}
