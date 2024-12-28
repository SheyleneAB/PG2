﻿using FitnessDomein.Model;
using FitnessDomein.Services;
using FitnessREST.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FitnessREST.Controllers
{
    [Route("api/[controller]/Equipment")]
    [ApiController]
    public class EquipmentController : Controller
    {
        private EquipmentService EquipmentService;
        private string url = "api/Equipment";
        public EquipmentController(EquipmentService equipmentservice)
        {
            this.EquipmentService = equipmentservice;
        }
        [HttpPost("/Equipment/Voegtoe")]
        public FitnessDomein.Model.Equipment VoegEquipmentToe([FromBody] EquipmentDTO equipment)
        {
            try
            {
                Equipment equipmentdm = new Equipment
                (
                    equipment.DeviceType,
                    equipment.IsInMaintenance
                    );
                return EquipmentService.VoegEquipmentToe(equipmentdm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

    }
}