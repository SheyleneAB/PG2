using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class RunningsessionDetailEF
{
    public int RunningsessionId { get; set; }

    public int SeqNr { get; set; }

    public int IntervalTime { get; set; }

    public double IntervalSpeed { get; set; }

    public virtual RunningsessionMainEF Runningsession { get; set; } = null!;
}
