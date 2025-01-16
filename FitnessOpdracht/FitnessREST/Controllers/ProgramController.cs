using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessDomein.Services;
using FitnessDomein.Model;
using FitnessREST.DTO;

namespace FitnessREST.Controllers
{
    [Route("api/[controller]/Program")]
    [ApiController]
   public class ProgramController : Controller
    {
        private ProgramService ProgramService;
        private string url = "api/Programs";
        public ProgramController(ProgramService programservice)
        {
            this.ProgramService = programservice;
        }

        [HttpPost("/Voegtoe")]
        public ActionResult<FitnessDomein.Model.Program> VoegProgramToe([FromBody] ProgramDTO program)
        {
            try
            {
                FitnessDomein.Model.Program programdm = new FitnessDomein.Model.Program
                (
                    program.ProgramCode,
                    program.Name,
                    program.Target,
                    program.StartDate,
                    program.MaxMembers
                );

                return Ok(ProgramService.VoegProgramToe(programdm));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/Update{Programcode}")]
        public ActionResult<FitnessDomein.Model.Program> UpdateProgram([FromRoute] string Programcode, [FromBody] ProgramDTO program)
        {
            if (string.IsNullOrEmpty(Programcode) || program == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                FitnessDomein.Model.Program programdm = new FitnessDomein.Model.Program
                (
                    program.ProgramCode,
                    program.Name,
                    program.Target,
                    program.StartDate,
                    program.MaxMembers
                );
                programdm.ProgramCode = Programcode;
                return Ok(ProgramService.UpdateProgram(programdm));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
