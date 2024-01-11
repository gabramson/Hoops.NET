using System;
using Hoops.Service;
using LanguageExt;
using Xunit;

namespace HoopsService.Tests.tournament
{
    public class HoopsService_AddTeamToField
    {
        [Fact]
        public void HoopsService_FieldShouldRetrieveAddedTeam()
        {
            var team = new Team(1, "DePaul", 1);
            var field = new Field();
            field.AddTeam(team);
            Option<Team> checkTeam = field.GetTeamById(1);
            Assert.True(checkTeam.IsSome);
            Assert.Equal(team, checkTeam);
        }
    }
}