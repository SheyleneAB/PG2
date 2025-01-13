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
    public class ReservationService
    {
        private IReservationRepositoryEF repo;
        public ReservationService(IReservationRepositoryEF repo)
        {
            this.repo = repo;
        }

        public void DeleteReservation(int id)
        {
            try
            {
                var reservation = repo.GeefReservation(id);
                if (reservation != null)
                {
                    repo.DeleteReservation(reservation);
                }
                else
                {
                    throw new ReservationServiceException($"Reservation with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ReservationServiceException("Error in Service during DeleteReservation", ex);
            }
        }

        public Reservation GeefReservation(int id)
        {
           try
            {
                return repo.GeefReservation(id);
            }
            catch (Exception ex)
            {
                throw new ReservationServiceException("GeefReservation", ex);
            }
        }

        public List<Reservation> GeefReservations()
        {
           return repo.GeefReservations();
        }

        public Reservation UpdateReservation(Reservation reservationdm)
        {
            var existingReservation = repo.GeefMemberReservations(reservationdm.Id.Value).FirstOrDefault(r => r.Id == reservationdm.Id);
            if (existingReservation != null)
            {
                existingReservation.Member = reservationdm.Member;
                existingReservation.Date = reservationdm.Date;

                // Clear existing time slots and add new ones
                existingReservation.ReservationTimeSlot.Clear();
                foreach (var timeSlot in reservationdm.ReservationTimeSlot)
                {
                    existingReservation.AddTimeSlot(timeSlot.TimeSlot, timeSlot.Equipment);
                }

                repo.UpdateReservation(existingReservation);
                return existingReservation;
            }
            return null;
        }

        public Reservation VoegReservationToe(Reservation reservationdm)
        {
            if (reservationdm == null)
            {
                throw new ArgumentNullException(nameof(reservationdm));
            }
            // TODO: check if reservation already exists
            repo.AddReservation(reservationdm);
            return reservationdm;
        }
        public List<Timeslot> GeefAlleOngebruikteTS (DateTime date, int equipmentId)
        {
            try
            {
                return repo.GetUnusedTimeSlots(date, equipmentId);
            }
            catch (Exception ex)
            {
                throw new ReservationServiceException("GeefAlleOngebruikteTS", ex);
            }
            
        }
    }
}
