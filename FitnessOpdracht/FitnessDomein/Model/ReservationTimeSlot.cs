using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class ReservationTimeSlot
    {
        public int ReservationTimeSlotId { get; set; }
        public int DeviceId { get; set; }
        public Timeslot TimeSlot { get; set; }
    }
}
