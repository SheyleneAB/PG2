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
    public class ReservationTimeSlotMapper
    {
        public static ReservationTimeSlot MapToDomain(ReservationTimeSlotEF db)
        {
            try
            {
                return new ReservationTimeSlot
                {
                    Id = db.ReservationTimeSlotId,
                    Equipment = EquipmentMapper.MapToDomain(db.Equipment),
                    TimeSlot = TimeSlotMapper.MaptoDomain(db.TimeSlot)
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapReservationTimeSlot - MapToDomain", ex);
            }
        }
        public static ReservationTimeSlotEF MapToDB(ReservationTimeSlot r)
        {
            try
            {
                return new ReservationTimeSlotEF
                {
                    ReservationTimeSlotId = r.Id.HasValue ? (int)r.Id : 0,
                    EquipmentId = (int)r.Equipment.Id,
                    TimeSlotId = (int)r.TimeSlot.Id
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapReservationTimeSlot - MapToDB", ex);
            }
        }
    }
}
