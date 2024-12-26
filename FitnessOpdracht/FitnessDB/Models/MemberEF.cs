using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDB.Models;

public partial class MemberEF
{
    private int? id;
    private DateTime birthday;
    private DateOnly dateOnly;

    public MemberEF()
    {
    }

    public MemberEF(int? id, string firstName, string lastName, string? email, string address, DateTime birthday, string? interests, string? memberType)
    {
        this.id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        this.birthday = birthday;
        Interests = interests;
        Membertype = memberType;
    }

    

    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MemberId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string? Interests { get; set; }

    public string? Membertype { get; set; }

    public virtual ICollection<CyclingsessionEF> Cyclingsessions { get; set; } = new List<CyclingsessionEF>();

    public virtual ICollection<ReservationEF> Reservations { get; set; } = new List<ReservationEF>();

    public virtual ICollection<RunningsessionMain> RunningsessionMains { get; set; } = new List<RunningsessionMain>();

    public virtual ICollection<ProgramEF> ProgramCodes { get; set; } = new List<ProgramEF>();
}
