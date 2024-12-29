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
        public List<Reservation> GeefMemberReservations(int memberId);
    }
}
