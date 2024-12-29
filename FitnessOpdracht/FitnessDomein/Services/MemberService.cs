using FitnessDomein.Exceptions;
using FitnessDomein.Model;
using FitnessDomein.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDomein.Services
{
    public class MemberService
    {
        private IMemberRepositoryEF repo;
        private IReservationRepositoryEF repoRes;
        private IProgramRepositoryEF repoProg;
        public MemberService(IMemberRepositoryEF repo, IReservationRepositoryEF repoRes, IProgramRepositoryEF repoProg)
        {
            this.repo = repo;
            this.repoRes = repoRes;
            this.repoProg = repoProg;
        }
        public Member GeefMember(int id)
        {
            try
            {
                return repo.GeefMember(id);
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("GeefMember", ex);
            }
        }

        public Member VoegMemberToe (Member member)
        {
            try
            {
                if (member == null) throw new MemberServiceException("VoegMemberToe - member is null");
                if (repo.HeeftMember(member.LastName, member.Birthday, member.Address)) throw new MemberServiceException("VoegMemberToe - member bestaat reeds");
                repo.VoegMemberToe(member);
                return member;
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("VoegMemberToe", ex);
            }
        }
        public List<Member> GeefMembers()
        {
            try
            {
                return repo.GeefMembers();
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("GeefMembers", ex);
            }
        }
       
        public bool HeeftMember(string lastName, DateTime birthday, string address)
        {
            try
            {
                return repo.HeeftMember(lastName, birthday, address);
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("HeeftMember", ex);
            }
        }
        public Member UpdateMember(Member member)
        {
            try
            {
                if (member == null) throw new MemberServiceException("UpdateMember - member is null");
                Member memberDB = repo.GeefMember(member.Id.Value);
                if (member == memberDB) throw new MemberServiceException("UpdateMember - geen verschillen");
                repo.UpdateMember(member);
                return member;
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("UpdateMember", ex);
            }
        }
        public List<Reservation> GetReservationsForMember(int memberId)
        {
            try
            {
                return repoRes.GeefMemberReservations(memberId);
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("GetReservationsForMember", ex);
            }
        }

        public List<Program> GetProgramsForMember(int id)
        {
            try
            {
                return repoProg.GetProgramsForMember(id);
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("GetProgramsForMember", ex);
            }
        }
    }
}
