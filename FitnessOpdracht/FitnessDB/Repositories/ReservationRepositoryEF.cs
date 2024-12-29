using FitnessDB.Exceptions;
using FitnessDB.Mappers;
using FitnessDB.Models;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
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
        public ReservationRepositoryEF (string connectionString)
        {
            ctx = new GymTestContextEF (connectionString);
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
                List<ReservationEF> reservationEFs= ctx.Reservations.Where(x => x.MemberId == memberId).ToList();
                List<Reservation> reservations = new List<Reservation>();
                foreach (ReservationEF reservationEF in reservationEFs)
                {
                    reservations.Add(ReservationMapper.MapToDomain(reservationEF));
                }
                return reservations;
            }
            catch (Exception ex)
            {
                throw new ReservationRepositoryException("GeefMemberReservations", ex);
            }
        }
    }
}
