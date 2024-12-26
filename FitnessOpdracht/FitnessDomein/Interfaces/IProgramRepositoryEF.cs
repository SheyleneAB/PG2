using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Interfaces
{
    public interface IProgramRepositoryEF
    {
        public Program GeefProgram(string programCode);
        public bool HeeftProgram(string programCode);
        public void UpdateProgram(Program program);
        public void VoegProgramToe(Program program);
    }
}
