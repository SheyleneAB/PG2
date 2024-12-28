using FitnessDomein.Exceptions;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Services
{
    public class EquipmentService
    {
        private IEquipmentRepositoryEF repo;
        public EquipmentService(IEquipmentRepositoryEF repo)
        {
            this.repo = repo;
        }
        public Equipment GeefEquipment(int id)
        {
            try
            {
                return repo.GeefEquipment(id);
            }
            catch (Exception ex)
            {
                throw new EquipmentServiceException("GeefEquipment", ex);
            }
        }
        public Equipment VoegEquipmentToe(Equipment equipment)
        {
            try
            {
                if (equipment == null) throw new EquipmentServiceException("VoegEquipmentToe - equipment is null");
                repo.VoegEquipmentToe(equipment);
                return equipment;
            }
            catch (Exception ex)
            {
                throw new EquipmentServiceException("VoegEquipmentToe", ex);
            }
        }


       
    }
}
