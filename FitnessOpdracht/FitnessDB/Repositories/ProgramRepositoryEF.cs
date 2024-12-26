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

    public class ProgramRepositoryEF : IProgramRepositoryEF
    {
        private GymTestContextEF ctx;
        public ProgramRepositoryEF(string connectionString)
        {
            ctx = new GymTestContextEF(connectionString);
        }
        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public void VoegProgramToe(Program program)
        {
            try
            {
                if (string.IsNullOrEmpty(program.programCode))
                {
                    program.programCode = Guid.NewGuid().ToString(); // Generate a new unique code
                }

                var programEntity = ProgramMapper.MapToDB(program);
                ctx.Programs.Add(programEntity);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new ProgramRepositoryException("VoegProgramToe", ex);
            }
        }
        public void UpdateProgram(Program program)
        {
            try
            {
                ctx.Programs.Update(ProgramMapper.MapToDB(program));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new ProgramRepositoryException("UpdateProgram", ex);
            }
        }

        public Program GeefProgram(string programCode)
        {
            try
            {
                var programEF = ctx.Programs.FirstOrDefault(p => p.ProgramCode == programCode);
                if (programEF == null)
                {
                    throw new ProgramRepositoryException($"Program with code {programCode} not found.");
                }
                return ProgramMapper.MapToDomain(programEF);
            }
            catch (Exception ex)
            {
                throw new ProgramRepositoryException("GeefProgram", ex);
            }
        }

        public bool HeeftProgram(string programCode)
        {
            try
            {
                return ctx.Programs.Any(p => p.ProgramCode == programCode);
            }
            catch (Exception ex)
            {
                throw new ProgramRepositoryException("HeeftProgram", ex);
            }
        }

    }
   
}
