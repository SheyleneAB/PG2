using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Equipment
    {
        public Equipment()
        {
        }
        public Equipment( string deviceType, bool isInMaintenance)
        {
            DeviceType = deviceType;
            IsInMaintenance = isInMaintenance;
        }
        public Equipment(int? id, string deviceType, bool isInMaintenance)
        {
            Id = id;
            DeviceType = deviceType;
            IsInMaintenance = isInMaintenance;
        }

        public int? Id { get; set; }
        public string DeviceType { get; set; }
        public bool IsInMaintenance { get; set; }
    }
}
