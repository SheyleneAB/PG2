using FitnessDL.Model;
using System;
using System.Collections.Generic;

namespace FitnessDL.Model;

public partial class RunningsessionMain
{
    public int RunningsessionId { get; set; }

    public DateTime Date { get; set; }

    public int MemberId { get; set; }

    public int Duration { get; set; }

    public double AvgSpeed { get; set; }

    public virtual MemberEF Member { get; set; } = null!;

    public virtual ICollection<RunningsessionDetail> RunningsessionDetails { get; set; } = new List<RunningsessionDetail>();
}
