using System;
using System.Collections.Generic;

namespace testlibrary.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string DeviceType { get; set; } = null!;

    public bool? IsInMaintenance { get; set; }

    public virtual ICollection<ReservationTimeSlot> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlot>();
}
