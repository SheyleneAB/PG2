﻿using System;
using System.Collections.Generic;

namespace FitnessDL.Model;

public partial class EquipmentEF
{
    public int EquipmentId { get; set; }

    public string DeviceType { get; set; } = null!;

    public bool? IsInMaintenance { get; set; }

    public virtual ICollection<ReservationTimeSlot> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlot>();

    public virtual ICollection<ReservationEF> Reservations { get; set; } = new List<ReservationEF>();
}
