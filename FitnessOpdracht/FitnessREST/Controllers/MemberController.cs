using Microsoft.AspNetCore.Mvc;
using FitnessDomein.Services;
using FitnessREST.DTO;
using FitnessDomein.Model;

namespace FitnessREST.Controllers
{
    [Route("api/[controller]/Member")]
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
        [HttpPost("/Member/Voegtoe")]
        public ActionResult<FitnessDomein.Model.Member> VoegMemberToe([FromBody] MemberDTO member)
        {
            try
            {
                FitnessDomein.Model.Member memberdm = new FitnessDomein.Model.Member
                (
                    member.FirstName,
                    member.LastName,
                    member.Email,
                    member.Address,
                    member.Birthday,
                    member.Interests,
                    member.MemberType

                    );

                return Ok(MemberService.VoegMemberToe(memberdm));

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
                return MemberService.GeefMember(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/Members")]
        public List<Member> GetMembers ()
        {
            try
            {
                return MemberService.GeefMembers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut("/Update/{id}")]
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
        public List<Reservation> GetMemberReservations([FromRoute] int id)
        {
            try
            {
                return MemberService.GetReservationsForMember(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/GetMemberPrograms/{id}")]
        public List<FitnessDomein.Model.Program> GetMemberPrograms([FromRoute] int id)
        {
            try
            {
                return MemberService.GetProgramsForMember(id);
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
