using FitnessDomein.Model;
using FitnessDomein.Services;
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
    }
}
