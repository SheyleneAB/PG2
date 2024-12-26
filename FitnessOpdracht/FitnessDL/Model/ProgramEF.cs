using System;
using System.Collections.Generic;

namespace FitnessDL.Model;

public partial class ProgramEF
{
    public string ProgramCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Target { get; set; } = null!;

    public DateTime Startdate { get; set; }

    public int MaxMembers { get; set; }

    public virtual ICollection<MemberEF> Members { get; set; } = new List<MemberEF>();
}
