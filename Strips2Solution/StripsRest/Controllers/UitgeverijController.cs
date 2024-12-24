using Microsoft.AspNetCore.Mvc;
using StripsBL.Model;
using StripsBL.Services;

namespace StripsRest.Controllers
{
    [ApiController]
    [Route("api/[controller]/Uitgeverij")]
    public class UitgeverijController : Controller
    {
        private StripService StripService;

        private string url = "api/Uitgeverij";

        public UitgeverijController(StripService stripservice)
        {
            this.StripService = stripservice;
        }
        [HttpPut("UpdateStrip/{StripId}")]
        public IActionResult UpdateStripUitgeverij(int Id, [FromBody] Uitgeverij uitgeverij)
        {
            if (uitgeverij == null)
            {
                return BadRequest("uitgeverij is null");
            }
            if (!StripService.HeeftStrip(Id))
            {
                return NotFound("Strip not found");
            }
            Strip strip = StripService.GeefStrip(Id);
            strip.Id = Id;
            StripService.VeranderUitgever(strip, uitgeverij);
            return NoContent();
        }
        
        [HttpPut("Update/{Id}")]
        public IActionResult UpdateUitgeverijgeg(int Id,[FromBody] Uitgeverij uitgeverij)
        {
            if (uitgeverij == null)
            {
                return BadRequest("uitgeverij is null");
            }
            if (!StripService.HeeftUitgeverijId(Id))
            {
                return NotFound("Uitgeverij not found");
            }
            uitgeverij.UitgeverijId = Id;
            StripService.UpdateUitgeverijgeg(uitgeverij);
            return NoContent();
        }
        

    }
}
