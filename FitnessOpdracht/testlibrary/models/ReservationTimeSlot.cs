using System;
using System.Collections.Generic;

namespace testlibrary.Models;

public partial class ReservationTimeSlot
{
    public int ReservationTimeSlotId { get; set; }

    public int ReservationId { get; set; }

    public int TimeSlotId { get; set; }

    public int EquipmentId { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
