﻿using FitnessDB.Models;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessDB.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitnessDB.Mappers
{
    public class EquipmentMapper
    {
        public static Equipment MapToDomain(EquipmentEF db)
        {
            try
            {
                return new Equipment
                {
                    Id = db.EquipmentId,
                    DeviceType = db.DeviceType,
                    IsInMaintenance = (bool)db.IsInMaintenance
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapEquipment - MapToDomain", ex);
            }
        }
        public static EquipmentEF MapToDB(Equipment e)
        {
            try
            {
                return new EquipmentEF
                {
                    EquipmentId = e.Id.HasValue ? (int)e.Id : 0,
                    DeviceType = e.DeviceType,
                    IsInMaintenance = e.IsInMaintenance
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapEquipment - MapToDB", ex);
            }
        }
        /*
        public static EquipmentEF MapToDB(Equipment e, DbContext ctx)
        {
            try
            {
                var existingEquipment = ctx.Equipment.Find(e.Id);
                if (existingEquipment != null)
                {
                    return existingEquipment;
                }

                
                return new EquipmentEF
                {
                    EquipmentId = e.Id.HasValue ? (int)e.Id : 0,
                    DeviceType = e.DeviceType,
                    IsInMaintenance = e.IsInMaintenance
                };
            }
            catch (Exception ex)
            {
                throw new MapException("MapEquipment - MapToDB", ex);
            }
        }

        */
    }
}
