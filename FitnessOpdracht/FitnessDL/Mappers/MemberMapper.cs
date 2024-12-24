using FitnessDL.Exceptions;
using FitnessDL.Model;
using FitnessDomein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDL.Mappers
{
    public class MemberMapper
    {
        public static Member MapToDomain(MemberEF db)
        {
            try
            {
                return new Member(db.Id, db.FirstName, db.LastName, db.Email, db.Address, db.Birthday, db.Interests, db.MemberType);
            }
            catch (Exception ex)
            {
                throw new MapException("MapMember - MapToDomain", ex);
            }
        }
        public static MemberEF MapToDB(Member m)
        {
            try
            {
                return new MemberEF(m.Id, m.FirstName, m.LastName, m.Email, m.Address, m.Birthday, m.Interests, m.MemberType);
            }
            catch (Exception ex)
            {
                throw new MapException("MapMember - MapToDB", ex);
            }
        }
    }
}
