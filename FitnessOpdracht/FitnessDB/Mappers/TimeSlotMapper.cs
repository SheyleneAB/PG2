﻿using FitnessDB.Exceptions;
using FitnessDB.Models;
using FitnessDomein.Model;

namespace FitnessDB.Mappers
{
    public class TimeSlotMapper
    {
        public static TimeSlot MapToDB(Timeslot timeSlot)
        {
            try
            {
                return new TimeSlot
                {
                    TimeSlotId = (int)timeSlot.Id,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    PartOfDay = timeSlot.PartOfDay
                };
            }
            catch (System.Exception ex)
            {
                throw new MapException("MapTimeSlot - MapToDB", ex);
            }
        }

        public static Timeslot MaptoDomain(TimeSlot timeSlot)
        {
            try
            {
                return new Timeslot
                {
                    Id = timeSlot.TimeSlotId,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    PartOfDay = timeSlot.PartOfDay
                };
            }
            catch (System.Exception ex)
            {
                throw new MapException("MapTimeSlot - MapToDomain", ex);
            }
        }
    }
}