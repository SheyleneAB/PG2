using FitnessDL.Exceptions;
using FitnessDL.Mappers;
using FitnessDL.Model;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDL.Repositories
{
    public class MemberRepositoryEF : IMemberRepositoryEF
    {
        private FitnessContext ctx;
        public MemberRepositoryEF(string connectionString)
        {
            ctx = new FitnessContext(connectionString);
        }
        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public Member GeefMember(int id)
        {
            try
            {
                return MemberMapper.MapToDomain(ctx.Member.Where(x => x.Id == id).AsNoTracking().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("GeefMember", ex);
            }
        }
        public List<Member> GeefMembers()
        {
            try
            {
                return ctx.Member.Select(x => MemberMapper.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("GeefMembers", ex);
            }
        }
        public void UpdateMember(Member member)
        {
            try
            {
                ctx.Member.Update(MemberMapper.MapToDB(member));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("UpdateMember", ex);
            }
        }
        public void VerwijderMember(int id)
        {
            try
            {
                ctx.Member.Remove(new MemberEF() { Id = id });
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("VerwijderMember", ex);
            }
        }
        public void VoegMemberToe(Member member)
        {
            try
            {
                ctx.Member.Add(MemberMapper.MapToDB(member));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("VoegMemberToe", ex);
            }
        }

        public bool HeeftMember(int id)
        {
            try
            {
                return ctx.Member.Any(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("HeeftMember", ex);
            }
        }
    }
}
