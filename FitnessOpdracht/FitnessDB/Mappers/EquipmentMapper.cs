using FitnessDB.Models;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessDB.Exceptions;

namespace FitnessDB.Mappers
{
    public class EquipmentMapper
    {
        public static Equipment MapToDomain(EquipmentEF db)
        {
            try
            {
                return new Equipment
                {
                    Id = db.EquipmentId,
                    DeviceType = db.DeviceType,
                    IsInMaintenance = (bool)db.IsInMaintenance
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapEquipment - MapToDomain", ex);
            }
        }
        public static EquipmentEF MapToDB(Equipment e)
        {
            try
            {
                return new EquipmentEF
                {
                    EquipmentId = (int)e.Id,
                    DeviceType = e.DeviceType,
                    IsInMaintenance = e.IsInMaintenance
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapEquipment - MapToDB", ex);
            }
        }


    }
}
