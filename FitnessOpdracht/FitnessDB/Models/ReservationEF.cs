﻿using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class ReservationEF
{
    public int ReservationId { get; set; }
 
    public DateTime Date { get; set; }

    public int MemberId { get; set; }

    public virtual MemberEF Member { get; set; } = null!;

    public virtual ICollection<ReservationTimeSlotEF> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlotEF>();

}
