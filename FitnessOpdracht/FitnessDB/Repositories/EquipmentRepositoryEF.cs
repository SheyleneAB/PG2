using FitnessDomein.Model;
using FitnessDomein.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessDB.Mappers;
using FitnessDB.Models;
using FitnessDB.Exceptions;

namespace FitnessDB.Repositories
{
    public class EquipmentRepositoryEF : IEquipmentRepositoryEF
    {
        private GymTestContextEF ctx;
        public EquipmentRepositoryEF(string connectionString)
        {
            ctx = new GymTestContextEF(connectionString);
        }
        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public Equipment GeefEquipment(int id)
        {
            try
            {
                return EquipmentMapper.MapToDomain(ctx.Equipment.Where(x => x.EquipmentId == id).AsNoTracking().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new EquipmentRepositoryException("GeefEquipment", ex);
            }
        }


        public void VoegEquipmentToe(Equipment equipment)
        {
            try
            {
                ctx.Equipment.Add(EquipmentMapper.MapToDB(equipment));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new EquipmentRepositoryException("VoegEquipmentToe", ex);
            }
        }

        public bool HeeftEquipment(int equipmentId)
        {
            try
            {
                return ctx.Equipment.Any(x => x.EquipmentId == equipmentId);
            }
            catch (Exception ex)
            {
                throw new EquipmentRepositoryException("HeeftEquipment", ex);
            }
        }
    }

}
