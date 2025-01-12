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
    }
}
