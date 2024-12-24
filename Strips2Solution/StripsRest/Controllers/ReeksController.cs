using Microsoft.AspNetCore.Mvc;
using StripsBL.Model;
using StripsBL.Services;

namespace StripsRest.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/reeks")]
    public class ReeksController : Controller
    {
        private StripService StripService;

        private string url = "api/Reeks";

        public ReeksController(StripService stripservice)
        {
            this.StripService = stripservice;
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateStripReeks(int Id, [FromBody] Reeks reeks)
        {
            if (reeks == null)
            {
                return BadRequest("reeks is null");
            }
            if (!StripService.HeeftStrip(Id))
            {
                return NotFound("Strip not found");
            }
            Strip strip = StripService.GeefStrip(Id);
            strip.Id = Id;
            StripService.VeranderReeks(strip, reeks);
            return NoContent();
        }
        [HttpGet("{Id}")]
        public IActionResult GetReeks(int Id)
        {
            if (!StripService.HeeftReeksId(Id))
            {
                return NotFound("Strip not found");
            }
            List<Strip> ReeksStrips = StripService.GeefReeks(Id);
            return Ok(ReeksStrips);
        }
    }
}
