using FitnessDB.Exceptions;
using FitnessDB.Mappers;
using FitnessDB.Models;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Repositories
{
    public class TimeSlotRepositoryEF : ITimeSlotRepositoryEF
    {
        private GymTestContextEF ctx;
        public TimeSlotRepositoryEF(string connectionString)
        {
            ctx = new GymTestContextEF(connectionString);
        }
        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public Timeslot GeefTimeSlot(int timeSlotId)
        {
            try
            {
                var timeSlotEF = ctx.TimeSlots
                    .Where(x => x.TimeSlotId == timeSlotId)
                    .FirstOrDefault();
                return TimeSlotMapper.MaptoDomain(timeSlotEF);
            }
            catch (Exception ex)
            {
                throw new TimeSlotRepositoryException("GeefTimeSlot", ex);
            }
        }
    }
}
