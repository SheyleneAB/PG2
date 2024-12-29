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
        public List<Member> SetRepairEquipment(int equipmentId)
        {
            try
            {
                var equipment = ctx.Equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);
                if (equipment == null)
                    throw new KeyNotFoundException($"Equipment with ID {equipmentId} not found.");

                equipment.IsInMaintenance = true;

                var futureReservationTimeSlots = ctx.ReservationTimeSlots
                    .Include(rts => rts.Reservation)
                    .ThenInclude(r => r.Member)
                    .Where(rts => rts.EquipmentId == equipmentId && rts.Reservation.Date > DateTime.Now)
                    .ToList();

                var members = futureReservationTimeSlots
                    .Select(rts => rts.Reservation.Member)
                    .Distinct()
                    .ToList();

                List<Member> membersdm = new List<Member>();
                foreach (var member in members)
                {
                    membersdm.Add(MemberMapper.MapToDomain(member));
                }

                ctx.ReservationTimeSlots.RemoveRange(futureReservationTimeSlots);

                ctx.SaveChanges();

                return membersdm;
            }
            catch (Exception ex)
            {
                throw new Exception("Error setting InMaintenance and deleting future reservations.", ex);
            }
        }

    }

}
