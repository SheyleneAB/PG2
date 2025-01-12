using FitnessDB.Models;
using FitnessDomein.Model;

namespace FitnessDB.Mappers
{
    internal class CyclingSessionMapper
    {
        public static Cyclingsession MapToDomain(CyclingsessionEF db)
        {
            try
            {
                return new Cyclingsession
                {
                    Id = db.CyclingsessionId,
                    Date = db.Date,
                    Comment = db.Comment,
                    Duration = db.Duration,
                    AvgWatt = db.AvgWatt,
                    MaxWatt = db.MaxWatt,
                    AvgCadence = db.AvgCadence,
                    MaxCadence = db.MaxCadence,
                    Member = MemberMapper.MapToDomain(db.Member),
                    TrainingType = db.Trainingtype
                };
            }
            catch (System.Exception ex)
            {
                throw new Exceptions.MapException("MapCyclingSession - MapToDomain", ex);

            }
        }
        public static CyclingsessionEF MapToDB(Cyclingsession c)
        {
            try
            {
                return new CyclingsessionEF
                {
                    CyclingsessionId = c.Id.HasValue ? (int)c.Id : 0,
                    Date = c.Date,
                    Comment = c.Comment,
                    Duration = c.Duration,
                    AvgWatt = c.AvgWatt,
                    MaxWatt = c.MaxWatt,
                    AvgCadence = c.AvgCadence,
                    MaxCadence = c.MaxCadence,
                    MemberId = MemberMapper.MapToDB(c.Member).MemberId,
                    Trainingtype = c.TrainingType
                };
            }
            catch (System.Exception ex)
            {
                throw new Exceptions.MapException("MapCyclingSession - MapToDB", ex);
            }
        }
    }
}