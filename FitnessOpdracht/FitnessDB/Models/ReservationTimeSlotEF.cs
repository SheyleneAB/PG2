using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class ReservationTimeSlotEF
{
    public ReservationTimeSlotEF()
    {
       
    }
    public int ReservationTimeSlotId { get; set; }

    public virtual EquipmentEF Device { get; set; }

    public virtual ReservationEF ReservationTimeSlotNavigation { get; set; } 

    public virtual TimeSlot TimeSlot { get; set; } 
}
