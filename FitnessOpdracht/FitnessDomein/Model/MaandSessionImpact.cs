using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class MaandSessionImpact
    {
        public int Month { get; set; }
        public int SessionCount { get; set; }
        public object TotalImpact { get; set; }
    }
}
