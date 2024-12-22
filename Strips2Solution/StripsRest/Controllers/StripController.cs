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

        [HttpPost("{stripId}/auteurs")]
        public ActionResult VoegAuteurToe(int stripId, [FromBody] Auteur auteur)
        {
            try
            {
                StripService.VoegAuteurToe(stripId, auteur);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{stripId}/auteurs/{auteurId}")]
        public ActionResult VerwijderAuteur(int stripId, int auteurId)
        {
            try
            {
                StripService.VerwijderAuteur(stripId, auteurId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /*
          private readonly IStripsRepository _repository;

          public StripsController(IStripsRepository repository)
          {
              _repository = repository;
          }

          [HttpGet("{id}")]
          public ActionResult<StripDTO> GetStrip(int id)
          {
              var strip = _repository.GeefStrip(id);
              if (strip == null) return NotFound();

              var stripDTO = new StripDTO
              {
                  Id = strip.Id,
                  Titel = strip.Titel,
                  ReeksId = strip.Reeks?.Id,
                  UitgeverijId = strip.Uitgeverij.Id,
                  AuteurIds = strip.Auteurs.Select(a => a.Id.Value).ToList()
              };

              return Ok(stripDTO);
          }

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

          [HttpPut("{id}")]
          public IActionResult UpdateStrip(int id, StripDTO stripDTO)
          {
              var strip = _repository.GeefStrip(id);
              if (strip == null) return NotFound();

              strip.WijzigTitel(stripDTO.Titel);
              strip.WijzigReeks(_repository.GeefReeks(stripDTO.ReeksId));
              strip.WijzigUitgeverij(_repository.GeefUitgeverij(stripDTO.UitgeverijId));

              var huidigeAuteurs = strip.Auteurs.Select(a => a.Id).ToList();
              foreach (var auteurId in stripDTO.AuteurIds)
              {
                  if (!huidigeAuteurs.Contains(auteurId))
                  {
                      strip.VoegAuteurToe(_repository.GeefAuteur(auteurId));
                  }
              }

              foreach (var auteur in strip.Auteurs.ToList())
              {
                  if (!stripDTO.AuteurIds.Contains(auteur.Id.Value))
                  {
                      strip.VerwijderAuteur(auteur);
                  }
              }

              _repository.UpdateStrip(strip);

              return NoContent();
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
}
