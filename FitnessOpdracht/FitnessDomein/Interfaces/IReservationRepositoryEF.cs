using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Interfaces
{
    public interface IReservationRepositoryEF
    {
        void AddReservation(Reservation reservationdm);
        void DeleteReservation(Reservation reservation);
        public List<Reservation> GeefMemberReservations(int memberId);
        List<Reservation> GeefReservations();
        Reservation GeefReservation(int id);
        void UpdateReservation(Reservation existingReservation);
        public List<Timeslot> GetUnusedTimeSlots(DateTime date, int equipmentId);
    }
}
