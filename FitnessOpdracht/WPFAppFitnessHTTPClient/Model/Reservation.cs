using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAppFitnessHTTPClient.Model
{
    class Reservation
    {
        public Reservation() { }

        public Member Member { get; set; }
        public DateTime Date { get; set; }

        public List<ReservationTimeSlot> ReservationTimeSlots { get; set; }
        public override string ToString()
        {
            return $" \n\t{string.Join("\n\t",Member)}, {Date}, \n\t{string.Join("\n\t", ReservationTimeSlots)}";
        }

    }
}
