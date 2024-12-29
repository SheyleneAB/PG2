using FitnessDomein.Interfaces;
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
    }
}
