﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Member
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public DateTime Birthday{ get; set; }
        public string? Interests { get; set; }
        public string? MemberType { get; set; }

    }
}