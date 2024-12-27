using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class ReservationTimeSlot
    {
        public ReservationTimeSlot()
        {
        }
        public ReservationTimeSlot(Reservation reservation, Equipment equipment, Timeslot timeSlot)
        {
            Reservation = reservation;
            Equipment = equipment;
            TimeSlot = timeSlot;
        }
        public ReservationTimeSlot(int? id, Reservation reservation, Equipment equipment, Timeslot timeSlot)
        {
            Id = id;
            Reservation = reservation;
            Equipment = equipment;
            TimeSlot = timeSlot;
        }
        public int? Id { get; set; }
        public Reservation Reservation{ get; set; }
        public Equipment Equipment { get; set; }
        public Timeslot TimeSlot { get; set; }
    }
}
