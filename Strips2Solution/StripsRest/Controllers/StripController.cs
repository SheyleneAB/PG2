using Microsoft.AspNetCore.Mvc;
using StripsBL.Interfaces;
using StripsBL.Model;
using StripsBL.Services;

namespace StripsRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripController : Controller
    {
        private StripService StripService;

        private string url = "api/Strips";

        public StripController(StripService stripservice)
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
        [HttpPut("{Id}")]
        public IActionResult UpdateStripTitel(int Id, [FromBody] string striptitel)
        {
            if (striptitel == null)
            {
                return BadRequest("Striptitel is null");
            }
            if (!StripService.HeeftStrip(Id))
            {
                return NotFound("Strip not found");
            }
            Strip strip = StripService.GeefStrip(Id);
            strip.Titel = striptitel;
            strip.Id = Id;
            StripService.UpdateStripTitel(strip);
            return NoContent();
        }
    }


                /*
                  

                  [HttpPost]
                  public ActionResult<StripDTO> CreateStrip(StripDTO stripDTO)
                  {
                      var reeks = _repository.GeefReeks(stripDTO.ReeksId);
                      var uitgeverij = _repository.GeefUitgeverij(stripDTO.UitgeverijId);
                      var auteurs = stripDTO.AuteurIds.Select(id => _repository.GeefAuteur(id)).ToList();

                      var strip = new Strip(stripDTO.Titel, reeks, uitgeverij);
                      foreach (var auteur in auteurs)
                      {
                          strip.VoegAuteurToe(auteur);
                      }

                      _repository.SchrijfStrip(strip);

                      return CreatedAtAction(nameof(GetStrip), new { id = strip.Id }, stripDTO);
                  }

                  

                  [HttpDelete("{id}")]
                  public IActionResult DeleteStrip(int id)
                  {
                      var strip = _repository.GeefStrip(id);
                      if (strip == null) return NotFound();

                      _repository.VerwijderStrip(strip);

                      return NoContent();
                  }
                */
    
}
