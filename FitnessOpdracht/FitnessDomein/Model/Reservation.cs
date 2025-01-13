using FitnessDomein.Exceptions;
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
        private List<ReservationTimeSlot> reservationtimeslot = new List<ReservationTimeSlot>();

        public Reservation(Member member, DateTime date, List<ReservationTimeSlot> reservationTimeSlot)
        {
            Member = member;
            Date = date;
            ReservationTimeSlot = reservationTimeSlot;
        }

        public int? Id {  get; set; }
        public Member Member { get; set; }
        public DateTime Date { get; set; }

        public ICollection<ReservationTimeSlot> ReservationTimeSlot { get { return reservationtimeslot; }
            set
            {
                if (value == null || value.Count < 1) throw new DomeinException("Reservationtimeslot-setrestimeslot");
                ReservationTimeSlot = new List<ReservationTimeSlot>(value);
            }
        }

        public void AddTimeSlot(Timeslot timeSlot, Equipment equipment)
        {
            if (timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));

            reservationtimeslot.Add(new ReservationTimeSlot(equipment, timeSlot));
        }
        public void RemoveTimeSlot(Timeslot timeSlot, Equipment equipment)
        {
            if (timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            reservationtimeslot.Remove(new ReservationTimeSlot(equipment, timeSlot));
        }

    }
}
