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
    public class ReservationMapper
    {
        public static Reservation MapToDomain (ReservationEF db)
        {
            try
            {
                return new Reservation
                {
                    Id = db.ReservationId,
                    Date = db.Date
                    // ReservationTimeSlot = db.ReservationTimeSlot
                    //TODO: Add ReservationTimeSlot
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
                    ReservationId = (int)r.Id,
                    Date = r.Date
                    // ReservationTimeSlot = r.ReservationTimeSlot
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapReservation - MapToDB", ex);
            }
        }
                 
    }
}
