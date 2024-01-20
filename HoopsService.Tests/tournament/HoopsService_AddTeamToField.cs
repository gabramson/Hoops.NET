using HoopsService.src.tournament;
using HoopsService.common;
using LanguageExt;
using Xunit;

namespace HoopsService.Tests.tournament
{
    public class HoopsService_AddTeamToField
    {
        [Fact]
        public void HoopsService_FieldShouldRetrieveAddedTeamById()
        {
            var team = new Team(1, "DePaul", 1);
            var field = new Field();
            field.AddTeam(team);
            Option<Team> checkTeam = field.GetTeamById(1);
            Assert.Equal(team, checkTeam);
        }

        [Fact]
        public void HoopsService_FieldShouldRetrieveAddedTeamByName()
        {
            var team = new Team(1, "DePaul", 1);
            var field = new Field();
            field.AddTeam(team);
            Option<Team> checkTeam = field.GetTeamByName("DePaul");
            Assert.Equal(team, checkTeam);
        }

        [Fact]
        public void HoopsService_FieldShouldReturnNoneForMissingId()
        {
            var team = new Team(1, "DePaul", 1);
            var field = new Field();
            field.AddTeam(team);
            Option<Team> checkTeam = field.GetTeamById(2);
            Assert.Equal(checkTeam, Option<Team>.None);
        }

        [Fact]
        public void HoopsService_FieldShouldReturnNoneForMissingName()
        {
            var team = new Team(1, "DePaul", 1);
            var field = new Field();
            field.AddTeam(team);
            Option<Team> checkTeam = field.GetTeamByName("DePauw");
            Assert.Equal(checkTeam, Option<Team>.None);
        }

        [Fact]
        public void HoopsService_FieldShouldThrowDuplicateError()
        {
            var teamDePaul = new Team(1, "DePaul", 1);
            var teamDePauw = new Team(1, "DePauw", 2);
            var field = new Field();
            field.AddTeam(teamDePaul).Match(
                Left: error => Assert.Fail("Should not have returned an error."),
                Right: value => Assert.Equal(value, new Success()));
            field.AddTeam(teamDePauw).Match(
                Left: error => Assert.Equal("Team with Id 1 already exists in the field.", error.Message),
                Right: value => Assert.Fail("Should not have returned a success."));
        }
    }
}