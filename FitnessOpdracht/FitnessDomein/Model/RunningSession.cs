using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class RunningSession
    {
        public int Duration { get; set; }
        public double AvgSpeed { get; set; }
        public Member Member { get; set; }
        public int? RunningSessionId {  get; set; }
        public DateTime Date { get; set; }
        public List<RunningSessionDetail> RunningSessionDetails { get; set; }
    }
}
