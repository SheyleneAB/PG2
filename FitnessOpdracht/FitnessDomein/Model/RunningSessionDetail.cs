using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class RunningSessionDetail
    {
        public int SeqNR { get; set; }
        public int IntervalTime { get; set; }
        public double IntervalSpeed { get; set; }
        public RunningSession RunningSession { get; set; }
        // omgekeerd? runningsesdet in runses
    }
}
