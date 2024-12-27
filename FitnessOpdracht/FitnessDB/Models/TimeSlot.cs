using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class TimeSlot
{
    public int TimeSlotId { get; set; }

    public int StartTime { get; set; }

    public int EndTime { get; set; }

    public string PartOfDay { get; set; } = null!;

    public virtual ICollection<ReservationTimeSlotEF> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlotEF>();

    public virtual ICollection<ReservationEF> Reservations { get; set; } = new List<ReservationEF>();
}
