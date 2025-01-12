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
    public class RunningDetailMapper
    {
        public static RunningSessionDetail MapToDomain(RunningsessionDetailEF db)
        {
            try
            {
                return new RunningSessionDetail
                {
                    SeqNR = db.SeqNr,
                    IntervalTime = db.IntervalTime,
                    IntervalSpeed = db.IntervalSpeed
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapRunningDetail - MapToDomain", ex);
            }
        }
        public static RunningsessionDetailEF MapToDB(RunningSessionDetail r)
        {
            try
            {
                return new RunningsessionDetailEF
                {
                    SeqNr = r.SeqNR,
                    IntervalTime = r.IntervalTime,
                    IntervalSpeed = r.IntervalSpeed
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapRunningDetail - MapToDB", ex);
            }
        }

    }
}
