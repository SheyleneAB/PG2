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
        public MemberService(IMemberRepositoryEF repo)
        {
            this.repo = repo;
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
        public void VerwijderMember(int id)
        {
            try
            {
                if (!repo.HeeftMember(id)) throw new MemberServiceException("VerwijderMember - member bestaat niet");
                repo.VerwijderMember(id);
            }
            catch (Exception ex)
            {
                throw new MemberServiceException("VerwijderMember", ex);
            }
        }
        public Member VoegMemberToe (Member member)
        {
            try
            {
                if (member == null) throw new MemberServiceException("VoegMemberToe - member is null");
                if (repo.HeeftMember(member.Id.Value)) throw new MemberServiceException("VoegMemberToe - member bestaat reeds");
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
        public bool HeeftMember(int id)
        {
            try
            {
                return repo.HeeftMember(id);
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
                if (!repo.HeeftMember(member.Id.Value)) throw new MemberServiceException("UpdateMember - member bestaat niet");
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

        
    }
}
