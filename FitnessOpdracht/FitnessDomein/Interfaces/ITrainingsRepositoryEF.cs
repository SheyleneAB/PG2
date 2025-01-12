using FitnessDomein.Model;

namespace FitnessDomein.Interfaces
{
    public interface ITrainingsRepositoryEF
    {
        List<Cyclingsession> GeefCyclingSessionMember(int id);
        List<RunningSessionDetail> GeefRunningSessionDetail(int id);
        List<RunningSession> GeefRunningSessionMember(int id);
    }
}