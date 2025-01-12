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
                                    .Include(r => r.ReservationTimeSlots) // Include related time slots
                                    .Include(r => r.Member) // Include related member
                                    .ToList();
                return reservationEFs.Select(ReservationMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GeefReservations", ex);
            }
        }
        // TODO: rename parameter 
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
                                        .Include(r => r.Member) // Include related member
                                        .FirstOrDefault(r => r.ReservationId == id);
                if (reservationEF == null || reservationEF.Member == null)
                {
                    throw new InvalidOperationException("Reservation or Member cannot be null");
                }
                return ReservationMapper.MapToDomain(reservationEF);
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GeefReservation", ex);
            }
        }
    }
}
