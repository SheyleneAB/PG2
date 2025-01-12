using FitnessDB.Mappers;
using FitnessDB.Models;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Repositories
{
    public class TrainingsRepositoryEF : ITrainingsRepositoryEF
    {
        private GymTestContextEF ctx;
        public TrainingsRepositoryEF(string connectionString)
        {
            ctx = new GymTestContextEF(connectionString);
        }
        public List<Cyclingsession> GeefCyclingSessionMember(int id)
        {
           try
            {
                var cyclingSessionEFs = ctx.Cyclingsessions
                    .Where(x => x.MemberId == id)
                    .Include(c => c.Member)
                    .ToList();
                return cyclingSessionEFs.Select(CyclingSessionMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GeefCyclingSessionMember", ex);
            }
        }

        public List<RunningSessionDetail> GeefRunningSessionDetail(int id)
        {
            try
            {
                var runningSessionEFs = ctx.RunningsessionDetails
                                    .Where(x => x.RunningsessionId == id)
                                    .ToList();
                return runningSessionEFs.Select(RunningDetailMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GeefRunningSessionDetail", ex);
            }
        }

        public List<RunningSession> GeefRunningSessionMember(int id)
        {
            try
            {
                var runningSessionEFs = ctx.RunningsessionMains
                                    .Where(x => x.MemberId == id)
                                    .Include(r => r.Member)
                                    .ToList();
                return runningSessionEFs.Select(RunningMainMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GeefRunningSessionMember", ex);
            }
        }
        public List<RunningSession> GeefRunningSessionInMaandJaar(int memberId, int maand, int jaar)
        {
            try
            {
                var runningSessionEFs = ctx.RunningsessionMains
                                    .Where(x => x.MemberId == memberId && x.Date.Month == maand && x.Date.Year == jaar)
                                    .Include(r => r.Member)
                                    .OrderBy(s => s.Date)
                                    .ToList();
                return runningSessionEFs.Select(RunningMainMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GeefRunningSessionInMaandJaar", ex);
            }
        }

        public List<Cyclingsession> GeefCyclingSessionInMaandJaar(int memberId, int maand, int jaar)
        {
            try
            {
                var cyclingSessionEFs = ctx.Cyclingsessions
                    .Where(x => x.MemberId == memberId && x.Date.Month == maand && x.Date.Year == jaar)
                    .Include(c => c.Member)
                    .OrderBy(s => s.Date)
                    .ToList();
                return cyclingSessionEFs.Select(CyclingSessionMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GeefCyclingSessionInMaandJaar", ex);
            }
        }
        public SessionStats GeefSessionStatsVoorMember(int MemberId)
        {
            try
            {
                var sessions = ctx.RunningsessionMains
                                    .Where(rs => rs.MemberId == MemberId)
                                    .Select(rs => new { rs.Duration })
                                    .Union(ctx.Cyclingsessions
                                    .Where(cs => cs.MemberId == MemberId)
                                    .Select(cs => new { cs.Duration }))
                                    .ToList();


                if (!sessions.Any())
                {
                    return new SessionStats
                    {
                        TotalSessions = 0,
                        TotalDurationHours = 0,
                        LongestSession = TimeSpan.Zero,
                        ShortestSession = TimeSpan.Zero,
                        AverageDurationMinutes = 0
                    };
                }
                return new SessionStats
                {
                    TotalSessions = sessions.Count,
                    TotalDurationHours = (sessions.Sum(s => s.Duration)) / 60,
                    LongestSession = sessions.Max(s => s.Duration),
                    ShortestSession = sessions.Min(s => s.Duration),
                    AverageDurationMinutes = sessions.Average(s => s.Duration)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("GetSessionStatsForKlant", ex);
            }
        }
        public List<MaandSessionOverview> GetSessionsPerMonth(int memberId, int year)
        {
            try
            {
                var runningSessions = ctx.RunningsessionMains
                    .Where(rs => rs.MemberId == memberId && rs.Date.Year == year)
                    .Select(rs => new
                    {
                        rs.Date
                    });
                var cyclingSessions = ctx.Cyclingsessions
                    .Where(cs => cs.MemberId == memberId && cs.Date.Year == year)
                    .Select(cs => new
                    {
                        cs.Date
                    });

                var sessions = runningSessions
                    .Union(cyclingSessions)
                    .GroupBy(s => s.Date.Month)
                    .Select(g => new MaandSessionOverview
                    {
                        Month = g.Key,
                        SessionCount = g.Count()
                    })
                    .OrderBy(m => m.Month)
                    .ToList();

                return sessions;
            }
            catch (Exception ex)
            {
                throw new Exception("GetSessionsPerMonth", ex);
            }
        }


        /*
        public List<MaandSessionImpact> GetMonthlySessionImpactWithDetails(int klantId, int jaar)
        {
            return ctx.Cyclingsessions
                .Where(s => s.MemberId == klantId && s.Date.Year == jaar)
                .GroupBy(s => s.Date.Month)
                .Select(g => new MaandSessionImpact
                {
                    Month = g.Key,
                    Sessions = g.Select(session => new SessionImpactDetail
                    {
                        SessionId = session.SessionId,
                        DurationMinutes = session.DuurInMinuten,
                        AverageWattage = session.GemiddeldeWattage,
                        Impact = BerekenTrainingsImpact(session.GemiddeldeWattage, session.DuurInMinuten)
                    }).ToList()
                })
                .OrderBy(m => m.Month)
                .ToList();
        }
        public string BerekenTrainingsImpact(int gemiddeldeWattage, int duurInMinuten)
        {
            if (gemiddeldeWattage < 150)
            {
                if (duurInMinuten <= 90)
                    return "low";
                else
                    return "medium";
            }
            else if (gemiddeldeWattage >= 150 && gemiddeldeWattage <= 200)
            {
                return "medium";
            }
            else // gemiddeldeWattage > 200
            {
                return "high";
            }
        }
        */
    }


}
