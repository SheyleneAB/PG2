﻿
using FitnessDB.Models;
using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessDB.Exceptions;
using FitnessDB.Mappers;

namespace FitnessDB.Repositories
{

    public class MemberRepositoryEF : IMemberRepositoryEF
    {
        private GymTestContextEF ctx;
        public MemberRepositoryEF(string connectionString)
        {
            ctx = new GymTestContextEF(connectionString);
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
                return MemberMapper.MapToDomain(ctx.Members.Where(x => x.MemberId == id).AsNoTracking().FirstOrDefault());
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
                return ctx.Members.Select(x => MemberMapper.MapToDomain(x)).ToList();
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
                MemberEF dbMember = MemberMapper.MapToDB(member);
                dbMember.MemberId = (int)member.Id;
                ctx.Members.Attach(dbMember);
                ctx.Entry(dbMember).State = EntityState.Modified;
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
                ctx.Members.Remove(new MemberEF() { MemberId = id });
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
                ctx.Members.Add(MemberMapper.MapToDB(member));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("VoegMemberToe", ex);
            }
        }

        public bool HeeftMember(string lastName, DateTime birthday, string address)
        {
            try
            {
                return ctx.Members.Any(x => x.LastName == lastName && x.Birthday.Date == birthday.Date && x.Address == address);
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("HeeftMember", ex);
            }
        }
    }


    

}