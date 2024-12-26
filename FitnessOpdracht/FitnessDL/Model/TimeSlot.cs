using System;
using System.Collections.Generic;

namespace FitnessDL.Model;

public partial class TimeSlot
{
    public int TimeSlotId { get; set; }

    public int StartTime { get; set; }

    public int EndTime { get; set; }

    public string PartOfDay { get; set; } = null!;

    public virtual ICollection<ReservationTimeSlot> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlot>();

    public virtual ICollection<ReservationEF> Reservations { get; set; } = new List<ReservationEF>();
}
