using FitnessDomein.Exceptions;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Services
{
    public class TimeSlotService
    {
        private ITimeSlotRepositoryEF repo;
        public TimeSlotService(ITimeSlotRepositoryEF repo)
        {
            this.repo = repo;
        }
        public Timeslot GeefTimeSlot(int timeSlotId)
        {
            try
            {
                return repo.GeefTimeSlot(timeSlotId);
            }
            catch (Exception ex)
            {
                throw new TimeSlotServiceException("GeefTimeSlot", ex);
            }
        }
    }
}
