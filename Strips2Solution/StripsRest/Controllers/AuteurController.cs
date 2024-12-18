using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StripsBL.Model;
using StripsBL.Services;

namespace StripsRest.Controllers
{

    [Route("api/[controller]/Auteur")]
    [ApiController]
    public class AuteurController : ControllerBase
    {
        private StripService StripService;

        private string url = "api/Strips";

        public AuteurController(StripService stripservice)
        {
            this.StripService = stripservice;
        }

        [HttpGet("{Id}")]
        public ActionResult<Strip> Get(int Id)
        {
            try
            {
                return Ok(StripService.GeefStrip(Id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
