using FitnessDomein.Model;

namespace FitnessDomein.Interfaces
{
    public interface IMemberRepositoryEF
    {
        Member GeefMember(int id);
        List<Member> GeefMembers();
        bool HeeftMember(int id);
        void UpdateMember(Member member);
        void VerwijderMember(int id);
        void VoegMemberToe(Member member);
    }
}