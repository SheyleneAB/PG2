using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using FitnessREST.Controllers;
using Moq;

namespace FitnessTest
{
    public class MemberTest
    {
        private readonly MemberController membercontroller;
        private readonly Mock<IMemberRepositoryEF> mockmemberrepo;
        private readonly Mock<ITrainingsRepositoryEF> mockmembertrrepo;
        public MemberTest()
        {
            mockmemberrepo = new Mock<IMemberRepositoryEF>();
            membercontroller = new MemberController((FitnessDomein.Services.MemberService)mockmemberrepo.Object, (FitnessDomein.Services.TrainingsService)mockmembertrrepo.Object);

        }

        [Fact]
       public void TestGetMember()
        {
            mockmemberrepo.Setup(x => x.GeefMember(1)).Returns(new ActionResult<Member>());
            var result = membercontroller.GetMember(1);
            Assert.NotNull(result);
        }
        [Fact]
        public void TestGetMembers()
        {
            mockmemberrepo.Setup(x => x.GeefMembers()).Returns(new List<FitnessDomein.Model.Member>());
            var result = membercontroller.GetMembers();
            Assert.NotNull(result);
        }
        [Fact]
        public void TestVoegMemberToe()
        {
            mockmemberrepo.Setup(x => x.VoegMemberToe(It.IsAny<FitnessDomein.Model.Member>())).Returns(new FitnessDomein.Model.Member());
            var result = membercontroller.VoegMemberToe(new FitnessREST.DTO.MemberDTO());
            Assert.NotNull(result);
        }
    }
}