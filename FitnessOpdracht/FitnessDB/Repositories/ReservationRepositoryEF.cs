using FitnessDB.Exceptions;
using FitnessDB.Mappers;
using FitnessDB.Models;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Repositories
{
    public class ReservationRepositoryEF : IReservationRepositoryEF
    {
        private GymTestContextEF ctx;
        public ReservationRepositoryEF(string connectionString)
        {
            ctx = new GymTestContextEF(connectionString);
        }
        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public List<Reservation> GeefMemberReservations(int memberId)
        {
            try
            {
                var reservationEFs = ctx.Reservations

                    .Where(x => x.MemberId == memberId)
                    .Include(r => r.ReservationTimeSlots)
                    .Include(r => r.Member)
                    .ToList();

                return reservationEFs.Select(ReservationMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GeefMemberReservations", ex);
            }
        }

        public void AddReservation(Reservation reservationdm)
        {
            try
            {
                var reservationEF = ReservationMapper.MapToDB(reservationdm);
                ctx.Reservations.Add(reservationEF);
                SaveAndClear();
                reservationdm.Id = reservationEF.ReservationId;
                int reservationId = reservationEF.ReservationId;

                foreach (var tijdslot in reservationdm.ReservationTimeSlot)
                {
                    var tijdslotef = ReservationTimeSlotMapper.MapToDB(tijdslot);
                    tijdslotef.ReservationId = reservationId;
                    ctx.ReservationTimeSlots.Add(tijdslotef);
                }

                SaveAndClear();
                reservationEF.ReservationId = reservationId;

            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("AddReservation", ex);
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            try
            {
                var reservationEF = ctx.Reservations
                    .Include(r => r.ReservationTimeSlots)
                    .FirstOrDefault(r => r.ReservationId == reservation.Id);

                if (reservationEF != null)
                {
                    // Manually delete related ReservationTimeSlot records
                    ctx.ReservationTimeSlots.RemoveRange(reservationEF.ReservationTimeSlots);

                    // Delete the Reservation
                    ctx.Reservations.Remove(reservationEF);

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("Error in Repository during DeleteReservation", ex);
            }
        }
        
        public List<Reservation> GeefReservations()
        {
            try
            {
                var reservationEFs = ctx.Reservations
                                    .Include(r => r.ReservationTimeSlots) 
                                    .Include(r => r.Member) 
                                    .ToList();
                return reservationEFs.Select(ReservationMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GeefReservations", ex);
            }
        }
        public void UpdateReservation(Reservation existingReservation)
        {
            try
            {
                var reservationEF = ReservationMapper.MapToDB(existingReservation);
                ctx.Reservations.Update(reservationEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("UpdateReservation", ex);
            }
        }

        public Reservation GeefReservation(int id)
        {
            try
            {
                var reservationEF = ctx.Reservations
                    .Include(r => r.Member)
                    .Include(r => r.ReservationTimeSlots)
                        .ThenInclude(rts => rts.Equipment)
                    .Include(r => r.ReservationTimeSlots)
                        .ThenInclude(rts => rts.TimeSlot)
                    .FirstOrDefault(r => r.ReservationId == id);

                if (reservationEF == null)
                {
                    throw new InvalidOperationException($"Reservation with ID {id} not found.");
                }

                return ReservationMapper.MapToDomain(reservationEF);
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GeefReservation", ex);
            }
        }
        public List<Timeslot> allTimeSlots = new List<Timeslot>
        {
        new Timeslot (1, 8, 9, "Morning" ),
        new Timeslot (2, 9, 10, "Morning"),
        new Timeslot (3, 10, 11, "Morning"),
        new Timeslot (4, 11, 12, "Morning"),
        new Timeslot (5, 12, 13, "Afternoon"),
        new Timeslot (6, 13, 14, "Afternoon"),
        new Timeslot (7, 14, 15, "Afternoon"),
        new Timeslot (8, 15, 16, "Afternoon"),
        new Timeslot (9, 16, 17, "Afternoon"),
        new Timeslot (10, 17, 18, "Afternoon"),
        new Timeslot (11, 18, 19, "Evening"),
        new Timeslot (12, 19, 20, "Evening"),
        new Timeslot (13, 20, 21, "Evening"),
        new Timeslot (14, 21, 22, "Evening")
        };
        public List<Timeslot> GetUnusedTimeSlots(DateTime date, int equipmentId)
        {
            try
            {
                var usedTimeSlotIds = ctx.ReservationTimeSlots
                    .Where(rts => rts.Reservation.Date == date && rts.EquipmentId == equipmentId)
                    .Select(rts => rts.TimeSlotId)
                    .Distinct()
                    .ToList();

                var unusedTimeSlots = allTimeSlots
                    .Where(ts => !usedTimeSlotIds.Contains((int)ts.Id))
                    .ToList();

                return unusedTimeSlots;
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GetUnusedTimeSlots", ex);

            }
        }
    }
}
