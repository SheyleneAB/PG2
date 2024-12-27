using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class EquipmentEF
{
    public int EquipmentId { get; set; }

    public string DeviceType { get; set; } = null!;

    public bool? IsInMaintenance { get; set; }

    public virtual ICollection<ReservationTimeSlotEF> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlotEF>();

    public virtual ICollection<ReservationEF> Reservations { get; set; } = new List<ReservationEF>();
}
