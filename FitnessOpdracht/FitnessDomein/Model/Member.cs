using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Model
{
    public class Member
    {
        public Member(int? id, string? firstName, string lastName, string email, string? address, DateTime birthday, string? interests, string? memberType)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Birthday = birthday;
            Interests = interests;
            MemberType = memberType;
        }

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
