using FitnessDomein.Interfaces;
using FitnessDomein.Model;
using FitnessREST.Controllers;
using FitnessREST.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FitnessTest
{
    public class MemberTest
    {
        private readonly MemberController membercontroller;
        private readonly Mock<IMemberRepositoryEF> mockmemberrepo;
        private readonly Mock<FitnessDomein.Services.MemberService> mockMemberService;
        private readonly Mock<FitnessDomein.Services.TrainingsService> mockTrainingsService;

        public MemberTest()
        {
            mockmemberrepo = new Mock<IMemberRepositoryEF>();

            mockMemberService = new Mock<FitnessDomein.Services.MemberService>(mockmemberrepo.Object);

            membercontroller = new MemberController(mockMemberService.Object);
        }

        [Fact]
        public void TestGetMember()
        {
            var testMember = new Member { Id = 1, FirstName = "Oliver", LastName = "Coens", Email = "oliver.coens@telenet.com", Address = "Mariakerke", Birthday = new DateTime(1972, 03, 06), Interests = "", MemberType = "Gold" };

            mockmemberrepo.Setup(repo => repo.GeefMember(1)).Returns(testMember); 

            var result = membercontroller.GetMember(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public void TestVoegMemberToe()
        {
            var memberDto = new FitnessREST.DTO.MemberDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Street",
                Birthday = DateTime.Now.AddYears(-30),
                Interests = "Fitness",
                MemberType = "Regular"
            };

            var member = new FitnessDomein.Model.Member(
                memberDto.FirstName,
                memberDto.LastName,
                memberDto.Email,
                memberDto.Address,
                memberDto.Birthday,
                memberDto.Interests,
                memberDto.MemberType
            );

            mockMemberService.Setup(x => x.VoegMemberToe(It.IsAny<Member>())).Returns(member);

            var result = membercontroller.VoegMemberToe(memberDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(member, createdAtActionResult.Value);

        }
        [Fact]
        public void Post_ValidMember_ReturnsOk()
        {
            var memberDto = new FitnessREST.DTO.MemberDTO
            {
                FirstName = "Jessy",
                LastName = "Doe",
                Email = "Jessy.doe@example.com",
                Address = "123 Street",
                Birthday = DateTime.Now.AddYears(-30),
                Interests = "Fitness",
                MemberType = "Regular"
            };
            var response = membercontroller.VoegMemberToe(memberDto);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }
        [Fact]
        public void Post_Valid_ReturnsCorrect()
        {
            var memberDto = new FitnessREST.DTO.MemberDTO
            {
                FirstName = "Jake",
                LastName = "Doe",
                Email = "Jake.doe@example.com",
                Address = "123 Street",
                Birthday = DateTime.Now.AddYears(-30),
                Interests = "Fitness",
                MemberType = "Regular"
            };
            var response = membercontroller.VoegMemberToe(memberDto).Result as OkObjectResult;
            var item = response.Value as MemberDTO;

            Assert.IsType<Member>(item);
            Assert.Equal(memberDto.FirstName, item.FirstName);
            Assert.Equal(memberDto.Email, item.Email);
            Assert.Equal(memberDto.LastName, item.LastName);
            Assert.Equal(memberDto.Interests, item.Interests);
            Assert.Equal(memberDto.MemberType, item.MemberType);
            
            
            

        } 
    }
}