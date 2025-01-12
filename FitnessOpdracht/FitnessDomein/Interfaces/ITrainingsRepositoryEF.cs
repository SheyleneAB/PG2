using FitnessDomein.Model;

namespace FitnessDomein.Interfaces
{
    public interface ITrainingsRepositoryEF
    {
        List<Cyclingsession> GeefCyclingSessionMember(int id);
        List<RunningSessionDetail> GeefRunningSessionDetail(int id);
        List<RunningSession> GeefRunningSessionMember(int id);
        public List<RunningSession> GeefRunningSessionInMaandJaar(int memberId, int maand, int jaar);
        public List<Cyclingsession> GeefCyclingSessionInMaandJaar(int memberId, int maand, int jaar);
        public SessionStats GeefSessionStatsVoorMember(int MemberId);
        public List<MaandSessionOverview> GetSessionsPerMonth(int memberId, int year);
    }
}