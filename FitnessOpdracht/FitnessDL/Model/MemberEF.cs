using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDL.Model
{
    public class MemberEF
    {
        public MemberEF() { }

        public MemberEF(int? id, string? firstName, string lastName, string email, string? address, DateTime birthday, string? interests, string? memberType)
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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Id { get; set; }

        [Column(TypeName = "nvarchar(45)")]
        public string? FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Address { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Interests { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? MemberType { get; set; }

        
    }
}
