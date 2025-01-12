using FitnessDomein.Exceptions;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Services
{
    public class TrainingsService
    {
        private ITrainingsRepositoryEF repo;
        public TrainingsService(ITrainingsRepositoryEF repo)
        {
            this.repo = repo;
        }
        public List<Cyclingsession>GeefCyclingSessionMember(int id)
        {
            try
            {
                return repo.GeefCyclingSessionMember(id);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GeefCyclingSession", ex);
            }
        }

        public List<RunningSessionDetail> GeefRunningSessionDetail(int id)
        {
            try
            {
                return repo.GeefRunningSessionDetail(id);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GetRunningSessionDetail", ex);
            }
        }

        public List<RunningSession>GeefRunningSessionMember(int id)
        {
            try
            {
                return repo.GeefRunningSessionMember(id);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GetRunningSession", ex);
            }
        }
        public List<RunningSession> GeefRunningSessionInMaandJaar(int memberId, int maand, int jaar)
        {
            try
            {
                return repo.GeefRunningSessionInMaandJaar(memberId, maand, jaar);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GetRunningSessionInMaandJaar", ex);
            }
        }
        public List<Cyclingsession> GeefCyclingSessionInMaandJaar(int memberId, int maand, int jaar)
        {
            try
            {
                return repo.GeefCyclingSessionInMaandJaar(memberId, maand, jaar);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GetCyclingSessionInMaandJaar", ex);
            }
        }
        public SessionStats GeefSessionStatsVoorMember(int MemberId)
        {
            try
            {
                return repo.GeefSessionStatsVoorMember(MemberId);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GetSessionStatsVoorMember", ex);
            }
        }
        public List<MaandSessionOverview> GetSessionsPerMonth(int memberId, int year)
        {
            try
            {
                return repo.GetSessionsPerMonth(memberId, year);
            }
            catch (Exception ex)
            {
                throw new TrainingsServiceException("GetSessionsPerMonth", ex);
            }
        }

    }
}
