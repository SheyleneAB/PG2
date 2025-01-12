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

        public Reservation(Member member, DateTime date, List<ReservationTimeSlot> reservationTimeSlot)
        {
            Member = member;
            Date = date;
            ReservationTimeSlot = reservationTimeSlot;
        }

        public int? Id {  get; set; }
        public Member Member { get; set; }
        public DateTime Date { get; set; }

        public ICollection<ReservationTimeSlot> ReservationTimeSlot { get; set; }

        public void AddTimeSlot(Timeslot timeSlot, Equipment equipment)
        {
            if (timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));

            ReservationTimeSlot.Add(new ReservationTimeSlot(equipment, timeSlot));
        }


        // reservations in member 
        // lijst combinatie tijdslot en equipment

    }
}
