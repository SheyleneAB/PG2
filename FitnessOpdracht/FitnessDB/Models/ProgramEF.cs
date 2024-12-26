using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class ProgramEF
{
    public string? ProgramCode { get; set; } 

    public string? Name { get; set; }

    public string? Target { get; set; }

    public DateTime Startdate { get; set; }

    public int MaxMembers { get; set; }

    public virtual ICollection<MemberEF> Members { get; set; } = new List<MemberEF>();
}
