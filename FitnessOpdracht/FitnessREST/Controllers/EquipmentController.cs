using FitnessDomein.Model;
using FitnessDomein.Services;
using FitnessREST.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FitnessREST.Controllers
{
    [Route("api/Equipment")]
    [ApiController]
    public class EquipmentController : Controller
    {
        private EquipmentService EquipmentService;
        private string url = "api/Equipment";
        public EquipmentController(EquipmentService equipmentservice)
        {
            this.EquipmentService = equipmentservice;
        }
        [HttpPost]
        public ActionResult <Equipment> VoegEquipmentToe([FromBody] EquipmentDTO equipment)
        {
            try
            {
                Equipment equipmentdm = new Equipment
                (
                    equipment.DeviceType,
                    equipment.IsInMaintenance
                    );
                return Ok(EquipmentService.VoegEquipmentToe(equipmentdm));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id}/Repair")]
        public ActionResult <List<Member>> SetRepairEquipment([FromRoute]int id)
        {
            try
            {
                return Ok(EquipmentService.SetRepairEquipment(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("Available")]
        public ActionResult<List<Equipment>> GetEquipment()
        {
            try
            {
                return Ok(EquipmentService.GeefAllAvailableEquipment());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
