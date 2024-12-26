
using System;
using System.Collections.Generic;

namespace FitnessDL.Model;

public partial class ReservationTimeSlot
{
    public int ReservationTimeSlotId { get; set; }

    public int ReservationId { get; set; }

    public int TimeSlotId { get; set; }

    public int DeviceId { get; set; }

    public virtual EquipmentEF Device { get; set; } = null!;

    public virtual ReservationEF ReservationTimeSlotNavigation { get; set; } = null!;

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
