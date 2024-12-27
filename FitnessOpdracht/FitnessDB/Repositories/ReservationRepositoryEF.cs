using FitnessDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Repositories
{
    public class ReservationRepositoryEF
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

        /*
         
        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
         */
    }
}
