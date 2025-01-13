using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Interfaces
{
    public interface IEquipmentRepositoryEF
    {
        void VoegEquipmentToe(Equipment equipment);
        Equipment GeefEquipment(int equipmentId);
        bool HeeftEquipment(int equipmentId);
        public List<Member> SetRepairEquipment(int equipmentId);
        List<Equipment> GeefAllAvailableEquipment();
    }
}
