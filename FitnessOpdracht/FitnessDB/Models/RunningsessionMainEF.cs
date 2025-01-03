﻿using System;
using System.Collections.Generic;

namespace FitnessDB.Models;

public partial class RunningsessionMainEF
{
    public int RunningsessionId { get; set; }

    public DateTime Date { get; set; }

    public int MemberId { get; set; }

    public int Duration { get; set; }

    public double AvgSpeed { get; set; }

    public virtual MemberEF Member { get; set; } = null!;

    public virtual ICollection<RunningsessionDetailEF> RunningsessionDetails { get; set; } = new List<RunningsessionDetailEF>();
}
