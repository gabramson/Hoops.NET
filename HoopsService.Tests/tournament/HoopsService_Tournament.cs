using HoopsService.src.tournament;
using LanguageExt;

namespace HoopsService.Tests.tournament{
    public class HoopsService_Tournament{

        private Field MakeField(){
            //generate 64 random teams
            var field = new Field();
            for (int i = 0; i <= 63; i++){
                field.AddTeam(new Team(i+1, "Team " + (i + 1), (i % 16)+1));
            }
            return field;
        }

        [Fact]
        public void HoopsService_TournamentShouldCreateGames(){
            var field = MakeField();
            var tournament = new Tournament(field);
            var games = tournament.GetPlayableGames();
            Assert.Equal(32, games.Count);
        }
    }
}
