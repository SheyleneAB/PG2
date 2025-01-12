using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class SessionStats
    {
        public int TotalSessions { get; set; }
        public double TotalDurationHours { get; set; }
        public object LongestSession { get; set; }
        public object ShortestSession { get; set; }
        public object AverageSessionDuration { get; set; }
        public double AverageDurationMinutes { get; set; }
    }
}
