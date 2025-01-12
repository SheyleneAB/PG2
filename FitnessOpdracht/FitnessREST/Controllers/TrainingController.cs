using FitnessDomein.Model;
using FitnessDomein.Services;
using FitnessREST.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FitnessREST.Controllers
{
    [ApiController]
    [Route("api/[controller]/Training")]
    public class TrainingController : Controller
    {
        private TrainingsService TrainingsService;
        private string url = "api/Trainings";
        public TrainingController(TrainingsService trainingsService)
        {
            this.TrainingsService = trainingsService;
        }
        [HttpGet("Training/GeefTrainingDetail/{id}")]
        public ActionResult<List<RunningSessionDetail>> GetTraining([FromRoute] int id)
        {
            try
            {
                return Ok( TrainingsService.GeefRunningSessionDetail(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Training/GeefTrainingMemberDate/{memberId}/{maand}/{jaar}")]
        public ActionResult<TrainingDTO> GetTraining([FromRoute] int memberId, [FromRoute] int maand, [FromRoute] int jaar)
        {
            try
            {
                TrainingDTO trainingdata = new TrainingDTO
                {
                    Cyclingsessions = TrainingsService.GeefCyclingSessionInMaandJaar(memberId, maand, jaar),
                    Runningsessions = TrainingsService.GeefRunningSessionInMaandJaar(memberId, maand, jaar)
                };
            
                return Ok(trainingdata);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GeefSessionStatsMember/{id}")]
        public ActionResult<SessionStats> GetSessionStats([FromRoute] int id)
        {
            // niet ok omdat sommige sessies null kunnen zijn -> runningsessions kan niet bestaan bv
            try
            {
                return Ok(TrainingsService.GeefSessionStatsVoorMember(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Training/GetSessionsPerMonth/{memberId}/{year}")]
        public ActionResult<List<MaandSessionOverview>> GetSessionsPerMonth([FromRoute] int memberId, [FromRoute] int year)
        {
            try
            {
                return Ok(TrainingsService.GetSessionsPerMonth(memberId, year));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
