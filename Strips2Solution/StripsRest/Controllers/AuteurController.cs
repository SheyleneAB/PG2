using Microsoft.AspNetCore.Mvc;
using StripsBL.Services;

namespace StripsRest.Controllers
{
    [Route("api/[controller]/Straat")]
    [ApiController]
    public class AuteurController : ControllerBase
    {
        private StripService StripService;

        private string url = "api/Strips";

        public AuteurController(StripService stripservice)
        {
            this.StripService = stripservice;
        }

        [HttpGet("{gemeenteId}")]
        public ActionResult<GemeenteRESToutputDTO> GetGemeente(int gemeenteId)
        {
            Gemeente gemeente = StripService.GeefGemeente(gemeenteId);
            GemeenteRESToutputDTO dto = MapFromDomain.MapFromGemeenteDomein(url, gemeente, straatService);
            return Ok(dto);
        }
    }
}
