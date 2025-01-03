﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StripsBL.Interfaces;
using StripsBL.Model;
using StripsBL.Services;


namespace StripsRest.Controllers
{

    [Route("api/[controller]/Auteur")]
    [ApiController]
    public class AuteurController : Controller
    {
        private StripService StripService;

        private string url = "api/Strips";
        public AuteurController(StripService stripservice)
        {
            this.StripService = stripservice;
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
        public ActionResult VerwijderAuteur(int stripId, Auteur auteur)
        {
            try
            {
                StripService.VerwijderAuteur(stripId, auteur);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update/{Id}")]
        public IActionResult UpdateAuteurgeg(int Id, [FromBody] Auteur auteur)
        {
            if (auteur == null)
            {
                return BadRequest("Auteur is null");
            }
            if (!StripService.HeeftAuteurId(Id))
            {
                return NotFound("Uitgeverij not found");
            }
            auteur.Id = Id;
            StripService.UpdateAuteurgeg(auteur);
            return NoContent();
        }
    }
}
