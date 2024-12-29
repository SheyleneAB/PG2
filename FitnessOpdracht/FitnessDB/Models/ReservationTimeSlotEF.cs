using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class ReservationTimeSlotEF
{
    public ReservationTimeSlotEF()
    {
       
    }
    public int EquipmentId { get; set; } 
    public int ReservationTimeSlotId { get; set; }
    public int TimeSlotId { get; set; }
    public int ReservationId { get; set; }
    public virtual EquipmentEF Equipment { get; set; }

    public virtual ReservationEF Reservation { get; set; } 

    public virtual TimeSlotEF TimeSlot { get; set; }
    
}
