using FitnessDB.Exceptions;
using FitnessDB.Models;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDB.Mappers
{
    public class ProgramMapper
    {
        public static Program MapToDomain(ProgramEF db)
        {
            try
            {
                return new Program
                {
                    Name = db.Name,
                    ProgramCode = db.ProgramCode,
                    Target = db.Target,
                    MaxMembers = db.MaxMembers,
                    StartDate = db.Startdate,
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapProgram - MapToDomain", ex);
            }
        }

        public static ProgramEF MapToDB(Program p)
        {
            try
            {
                return new ProgramEF
                {
                    Name = p.Name,
                    ProgramCode = p.ProgramCode,
                    Target = p.Target,
                    MaxMembers = p.MaxMembers,
                    Startdate = p.StartDate,

                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapProgram - MapToDB", ex);
            }
        }
    }
}
