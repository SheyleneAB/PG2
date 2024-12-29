using FitnessDomein.Model;

namespace FitnessDomein.Interfaces
{
    public interface IMemberRepositoryEF
    {
        Member GeefMember(int id);
        List<Member> GeefMembers();
        
        public bool HeeftMember(string lastName, DateTime birthday, string address);
        void UpdateMember(Member member);
        void VerwijderMember(int id);
        void VoegMemberToe(Member member);
    }
}