using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class TimeSlotEF
{
    public int TimeSlotId { get; set; }

    public int StartTime { get; set; }

    public int EndTime { get; set; }

    public string PartOfDay { get; set; } = null!;

    public virtual ICollection<ReservationTimeSlotEF> ReservationTimeSlots { get; set; } 

    }
