using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Equipment
    {
        public int? Id { get; set; }
        public string DeviceType { get; set; }
        public bool IsInMaintenance { get; set; }
    }
}
