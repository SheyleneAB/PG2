﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Timeslot
    {
        public Timeslot()
        {
        }
        public Timeslot(int? id, int startTime, int endTime, string partOfDay)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            PartOfDay = partOfDay;
        }

        public int? Id {  get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string PartOfDay { get; set; }
    }
}
