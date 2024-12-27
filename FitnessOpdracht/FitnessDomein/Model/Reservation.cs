using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Reservation
    {
        public Reservation() { }
        public int? Id {  get; set; }
        public Member Member { get; set; }
        public Timeslot TimeSlot { get; set; }
        public DateTime Date { get; set; }
        public Equipment Equipment { get; set; }
        public List<ReservationTimeSlot> TimeSlots { get; set; }

        public void AddTimeSlot(Timeslot timeSlot, Equipment equipment)
        {
            TimeSlots.Add(new ReservationTimeSlot(this, equipment, timeSlot ));
        }

        // reservations in member 
        // lijst combinatie tijdslot en equipment

    }
}
