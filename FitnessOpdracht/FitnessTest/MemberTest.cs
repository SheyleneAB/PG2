using FitnessDomein.Interfaces;
using FitnessREST.Controllers;
using Moq;

namespace FitnessTest
{
    public class MemberTest
    {
        private readonly MemberController membercontroller;
        private readonly Mock<IMemberRepositoryEF> mockmemberrepo;
        public MemberTest()
        {
            mockmemberrepo = new Mock<IMemberRepositoryEF>();
            membercontroller = new MemberController((FitnessDomein.Services.MemberService)mockmemberrepo.Object);
        }

        [Fact]
        public void MemberControllerTest()
        {
            
        }
    }
}