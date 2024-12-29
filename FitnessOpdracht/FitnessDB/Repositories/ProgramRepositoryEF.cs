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
                // Check if the program exists
                var dbProgram = ctx.Programs.FirstOrDefault(p => p.ProgramCode == program.ProgramCode);

                if (dbProgram == null)
                    throw new KeyNotFoundException($"Program with code {program.ProgramCode} not found");

                // Update fields
                dbProgram.Name = program.Name;
                dbProgram.Target = program.Target;
                dbProgram.Startdate = program.StartDate;
                dbProgram.MaxMembers = program.MaxMembers;

                // Save changes
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ProgramRepositoryException("Error updating program", ex);
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

        public List<Program> GetProgramsForMember(int id)
        {
            try
            {
                return ctx.Programs.Where(p => p.Members.Any(m => m.MemberId == id)).Select(ProgramMapper.MapToDomain).ToList();
            }
            catch (Exception ex)
            {
                throw new ProgramRepositoryException("GetProgramsForMember", ex);
            }
        }
    }
   
}
