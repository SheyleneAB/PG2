using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class ReservationEF
{
    public int ReservationId { get; set; }

    public int EquipmentId { get; set; }

    public int TimeSlotId { get; set; }

    public DateTime Date { get; set; }

    public int MemberId { get; set; }

    public virtual EquipmentEF Equipment { get; set; } = null!;

    public virtual MemberEF Member { get; set; } = null!;

    public virtual ReservationTimeSlotEF? ReservationTimeSlot { get; set; }

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
