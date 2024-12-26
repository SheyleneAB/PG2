
using FitnessDL.Model;
using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class MemberEF
{
    public int MemberId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string? Interests { get; set; }

    public string? Membertype { get; set; }

    public virtual ICollection<CyclingsessionEF> Cyclingsessions { get; set; } = new List<CyclingsessionEF>();

    public virtual ICollection<ReservationEF> Reservations { get; set; } = new List<ReservationEF>();

    public virtual ICollection<RunningsessionMain> RunningsessionMains { get; set; } = new List<RunningsessionMain>();

    public virtual ICollection<ProgramEF> ProgramCodes { get; set; } = new List<ProgramEF>();
}
