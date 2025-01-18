using Microsoft.AspNetCore.Mvc;
using FitnessDomein.Services;
using FitnessREST.DTO;
using FitnessDomein.Model;

namespace FitnessREST.Controllers
{
    [Route("api/member")]
    [ApiController]
    public class MemberController : Controller
    {
        private MemberService MemberService;
        private TrainingsService TrainingsService;
        private string url = "api/Members";

        public MemberController(MemberService memberservice, TrainingsService trainingsService)
        {
            this.MemberService = memberservice;
            TrainingsService = trainingsService;
        }
        [HttpPost]
        public ActionResult<Member> VoegMemberToe([FromBody] MemberDTO member)
        {
            try
            {
                FitnessDomein.Model.Member memberdm = new 
                (
                    member.FirstName,
                    member.LastName,
                    member.Email,
                    member.Address,
                    member.Birthday,
                    member.Interests,
                    member.MemberType

                    );

                var createdMember = MemberService.VoegMemberToe(memberdm);
                return CreatedAtAction(nameof(GetMember), new { id = createdMember.Id }, createdMember);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/Member/{id}")]
        public ActionResult<Member> GetMember([FromRoute]int id)
        {
            try
            {
                return Ok(MemberService.GeefMember(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/Members")]
        public ActionResult<List<Member>> GetMembers ()
        {
            try
            {
                return Ok(MemberService.GeefMembers());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut("/Member/{id}")]
        public IActionResult UpdateMember([FromRoute] int id, [FromBody] MemberDTO member)
        {
            try
            {
                Member memberdm = new Member
                (
                    member.FirstName,
                    member.LastName,
                    member.Email,
                    member.Address,
                    member.Birthday,
                    member.Interests,
                    member.MemberType
                );

                memberdm.Id = id; 
                MemberService.UpdateMember(memberdm);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        [HttpGet("/GetMemberReservations/{id}")]
        public ActionResult<List<Reservation>> GetMemberReservations([FromRoute] int id)
        {
            try
            {
                return Ok(MemberService.GetReservationsForMember(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/GetMemberPrograms/{id}")]
        public ActionResult<List<FitnessDomein.Model.Program>> GetMemberPrograms([FromRoute] int id)
        {
            try
            {
                return Ok(MemberService.GetProgramsForMember(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/GetMemberTrainings/{id}")]
        public ActionResult<TrainingDTO> GetMemberTrainings([FromRoute] int id)
        {
            try
            {
                var RunningSessions = TrainingsService.GeefRunningSessionMember(id);
                var CyclingSessions = TrainingsService.GeefCyclingSessionMember(id);
                var trainingDTO = new TrainingDTO
                {
                    Runningsessions = RunningSessions,
                    Cyclingsessions = CyclingSessions
                };

                return Ok(trainingDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
