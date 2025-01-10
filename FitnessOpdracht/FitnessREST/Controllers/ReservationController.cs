using FitnessDomein.Model;
using FitnessDomein.Services;
using FitnessREST.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FitnessREST.Controllers
{
    [Route("api/[controller]/Reservation")]
    [ApiController]
    public class ReservationController : Controller
    {
        private ReservationService ReservationService;
        private MemberService MemberService;
        private EquipmentService EquipmentService;
        private TimeSlotService TimeSlotService;
        private string url = "api/Reservations";
        public ReservationController(ReservationService reservationservice, MemberService memberService, EquipmentService equipmentService, TimeSlotService timeSlotService)
        {
            this.ReservationService = reservationservice;
            MemberService = memberService;
            EquipmentService = equipmentService;
            TimeSlotService = timeSlotService;
        }


        [HttpGet("/Reservation/{id}")]
        public ActionResult<Reservation> GetReservation([FromRoute] int id)
        {
            try
            {
                var reservation = ReservationService.GeefReservation(id);
                if (reservation == null)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("/ReservationVoegtoe")]
        public Reservation VoegReservationToe([FromBody] ReservationDTO reservation)
        {
            try
            {
                var member = MemberService.GeefMember(reservation.MemberId);
                var reservationTimeSlots = reservation.ReservationTimeSlot
                    .Select(dto => new ReservationTimeSlot
                    {
                        Equipment = EquipmentService.GeefEquipment(dto.EquipmentId),
                        TimeSlot= TimeSlotService.GeefTimeSlot(dto.TimeSlotId)
                    }).ToList();

                Reservation reservationdm = new Reservation
                (
                    member,
                    reservation.Date,
                    reservationTimeSlots
                );
                return ReservationService.VoegReservationToe(reservationdm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("/Reservations")]
        public List<Reservation> GetReservations()
        {
            try
            {
                return ReservationService.GeefReservations();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetReservations: " + ex.Message, ex);

            }
        }
        [HttpDelete("/ReservationDelete/{id}")]
        public void DeleteReservation([FromRoute] int id)
        {
            try
            {
                ReservationService.DeleteReservation(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpPut("/ReservationUpdate/{id}")]
        public Reservation UpdateReservation([FromRoute] int id, [FromBody] ReservationDTO reservation)
        {
            try
            {
                var member = MemberService.GeefMember(reservation.MemberId);
                var reservationTimeSlots = reservation.ReservationTimeSlot
                    .Select(dto => new ReservationTimeSlot
                    {
                        Equipment = EquipmentService.GeefEquipment(dto.EquipmentId),
                        TimeSlot = TimeSlotService.GeefTimeSlot(dto.TimeSlotId)
                    }).ToList();

                Reservation reservationdm = new Reservation
                (
                    member,
                    reservation.Date,
                    reservationTimeSlots
                );
                reservationdm.Id = id;
                return ReservationService.UpdateReservation(reservationdm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }

}
