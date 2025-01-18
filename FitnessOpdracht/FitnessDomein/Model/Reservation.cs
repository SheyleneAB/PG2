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
        private DateTime date;
        public int? Id {  get; set; }
        public Member Member { get; set; }
        public DateTime Date
        {
            get => date;
            set
            {
                if (value <= DateTime.Now)
                    throw new DomeinException("Reservaties kunnen enkel in de toekomst gemaakt worden. ");

                if (value > DateTime.Now.AddDays(7))
                    throw new DomeinException("Reservaties kunnen maximum 1 week op voorhand gemaakt worden. ");

                date = value;
            }
        }

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
            if (reservationtimeslot.Any(rts => rts.TimeSlot.Id == timeSlot.Id && rts.Equipment.Id == equipment.Id))
            {
                throw new DomeinException("Deze tijdslot is al gereserveerd. ");
            }
            if (reservationtimeslot.Count >= 4)
            {
                throw new DomeinException("Er kunnen niet meer dan 4 tijdslotten per dag gereserveerd worden. ");
            }
            var equipmentSlots = reservationtimeslot
                .Where(rts => rts.Equipment.Id == equipment.Id)
                .OrderBy(rts => rts.TimeSlot.StartTime)
                .ToList();
            if (equipmentSlots.Count > 0)
            {
                var lastSlot = equipmentSlots.Last();
                if (lastSlot.TimeSlot.StartTime == timeSlot.StartTime - 1 || lastSlot.TimeSlot.StartTime == timeSlot.StartTime - 2)
                {
                    if (equipmentSlots.Count(ts => Math.Abs(ts.TimeSlot.StartTime - timeSlot.StartTime) <= 2) >= 2)
                    {
                        throw new DomeinException("Je kan de equipment niet meer dan 2 keer na elkaar gebruiken.");
                    }
                }
            }

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
