using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDB.Models;

public partial class EquipmentEF
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EquipmentId { get; set; }

    public string DeviceType { get; set; } = null!;

    public bool? IsInMaintenance { get; set; }

    public virtual ICollection<ReservationTimeSlotEF> ReservationTimeSlots { get; set; } = new List<ReservationTimeSlotEF>();

}
