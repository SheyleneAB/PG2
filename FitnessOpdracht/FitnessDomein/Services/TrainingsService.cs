using FitnessDomein.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Services
{
    public class TrainingsService
    {
        private ITrainingsRepositoryEF repo;
        public TrainingsService(ITrainingsRepositoryEF repo)
        {
            this.repo = repo;
        }
    }
}
