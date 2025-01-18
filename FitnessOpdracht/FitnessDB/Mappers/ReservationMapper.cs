using FitnessDB.Exceptions;
using FitnessDB.Models;
using FitnessDomein.Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Mappers
{
    public class ReservationMapper
    {
        public static Reservation MapToDomain(ReservationEF db)
        {
            try
            {
                return new Reservation
                {
                    Id = db.ReservationId,
                    Date = db.Date,
                    Member = MemberMapper.MapToDomain(db.Member),
                    ReservationTimeSlot = db.ReservationTimeSlots.Select(ReservationTimeSlotMapper.MapToDomain).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapReservation - MapToDomain", ex);
            }
        }

        public static ReservationEF MapToDB(Reservation r)
        {
            try
            {
                return new ReservationEF
                {
                    ReservationId = r.Id.HasValue ? (int)r.Id : 0,
                    Date = r.Date,
                    MemberId = (int)r.Member.Id 
                    
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapReservation - MapToDB", ex);
            }
        }
    }
}
