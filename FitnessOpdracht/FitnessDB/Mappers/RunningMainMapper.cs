using FitnessDB.Exceptions;
using FitnessDB.Models;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Mappers
{
    public class RunningMainMapper
    {
        public static RunningSession MapToDomain(RunningsessionMainEF db)
        {
            try
            {
                return new RunningSession
                {
                    RunningSessionId = db.RunningsessionId,
                    Date = db.Date,
                    Duration = db.Duration,
                    AvgSpeed = db.AvgSpeed,
                    Member = MemberMapper.MapToDomain(db.Member)
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapRunningSession - MapToDomain", ex);
            }
        }

        public static RunningsessionMainEF MapToDB(RunningSession dm)
        {
            try
            {
                return new RunningsessionMainEF
                {
                    RunningsessionId = dm.RunningSessionId ?? 0,
                    Date = dm.Date,
                    Duration = dm.Duration,
                    AvgSpeed = dm.AvgSpeed,
                    Member = MemberMapper.MapToDB(dm.Member)
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapRunningSession - MapToDB", ex);
            }
        }

    }
}
