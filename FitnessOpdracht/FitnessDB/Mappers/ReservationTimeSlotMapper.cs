﻿using FitnessDB.Exceptions;
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
                    Reservation = ReservationMapper.MapToDomain(db.ReservationTimeSlotNavigation),
                    Equipment = EquipmentMapper.MapToDomain(db.Device),
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
                    ReservationTimeSlotId = (int)r.Id,
                    ReservationTimeSlotNavigation = ReservationMapper.MapToDB(r.Reservation),
                    Device = EquipmentMapper.MapToDB(r.Equipment),
                    TimeSlot = TimeSlotMapper.MapToDB(r.TimeSlot)
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapReservationTimeSlot - MapToDB", ex);
            }
        }
    }
}