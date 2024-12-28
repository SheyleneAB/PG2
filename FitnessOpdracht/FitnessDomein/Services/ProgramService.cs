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
    public class ProgramService
    {
        private IProgramRepositoryEF repo;
        public ProgramService(IProgramRepositoryEF repo)
        {
            this.repo = repo;
        }
        public Program VoegProgramToe(Program program)
        {
            try
            {
                if (program == null) throw new ProgramServiceException("VoegProgramToe - program is null");
                if (repo.HeeftProgram(program.ProgramCode)) throw new ProgramServiceException("VoegProgramToe - program bestaat reeds");
                repo.VoegProgramToe(program);
                return program;
            }
            catch (Exception ex)
            {
                throw new ProgramServiceException("VoegProgramToe", ex);
            }
        }
        public Program UpdateProgram(Program program)
        {
            try
            {
                if (program == null) throw new ProgramServiceException("UpdateProgram - program is null");
                if (!repo.HeeftProgram(program.ProgramCode)) throw new ProgramServiceException("UpdateProgram - program bestaat niet");
                Program programDB = repo.GeefProgram(program.ProgramCode);
                if (program == programDB) throw new ProgramServiceException("UpdateProgram - geen verschillen");
                repo.UpdateProgram(program);
                return program;
            }
            catch (Exception ex)
            {
                throw new ProgramServiceException("UpdateProgram", ex);
            }
        }
        
    }
}
