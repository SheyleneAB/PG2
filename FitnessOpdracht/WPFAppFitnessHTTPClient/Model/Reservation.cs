using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAppFitnessHTTPClient.Model
{
    class Reservation
    {
        private List<ReservationTimeSlot> reservationtimeslot = new List<ReservationTimeSlot>();
        public Reservation() { }
        private DateTime date;
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Date
        {
            get => date;
            set
            {
                if (value <= DateTime.Now)
                    throw new Exception("Reservaties kunnen enkel in de toekomst gemaakt worden. ");

                if (value > DateTime.Now.AddDays(7))
                    throw new Exception("Reservaties kunnen maximum 1 week op voorhand gemaakt worden. ");

                date = value;
            }
        }

        public ICollection<ReservationTimeSlot> ReservationTimeSlot
        {
            get { return reservationtimeslot; }
            set
            {
                if (value == null || value.Count < 1) throw new Exception("Reservationtimeslot-setrestimeslot");
                ReservationTimeSlot = new List<ReservationTimeSlot>(value);
            }
        }

        public void AddTimeSlot(TimeSlot timeSlot, Equipment equipment)
        {

            if (timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            if (reservationtimeslot.Any(rts => rts.TimeSlot.Id == timeSlot.Id && rts.Equipment.Id == equipment.Id))
            {
                throw new Exception("Deze tijdslot is al gereserveerd. ");
            }
            if (reservationtimeslot.Count >= 4)
            {
                throw new Exception("Er kunnen niet meer dan 4 tijdslotten per dag gereserveerd worden. ");
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
                        throw new Exception("Je kan de equipment niet meer dan 2 keer na elkaar gebruiken.");
                    }
                }
            }
            reservationtimeslot.Add(new ReservationTimeSlot(equipment, timeSlot));
        }
        public void RemoveTimeSlot(TimeSlot timeSlot, Equipment equipment)
        {
            if (timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            reservationtimeslot.Remove(new ReservationTimeSlot(equipment, timeSlot));
        }
        public override string ToString()
        {
            return $" {MemberId}, {Date}, \n\t{string.Join("\n\t", ReservationTimeSlot)}";
        }

    }
}
