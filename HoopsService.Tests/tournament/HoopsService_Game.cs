using HoopsService.src.tournament;
using LanguageExt;

namespace HoopsService.Tests.tournament{
    public class HoopsService_Game{
        [Fact]
        public void HoopsService_NewGameShouldNotHaveWinner(){
            Game game = new Game();
            Assert.Equal(Option<Team>.None, game.Winner);
        }
        
        [Fact]
        public void HoopsService_GameShouldRecordWinner(){
            Team winner = new Team(1, "DePaul", 1);
            Team loser = new Team(2, "Kansas", 2);

            Game game = new Game();
            game.AddTeam(winner);
            game.AddTeam(loser);
            game.RecordWinner(winner);
            Assert.Equal(winner, game.Winner);
        }

        [Fact]
        public void HoopsService_GameShouldThrowErrorForDuplicateTeam(){
            Team winner = new Team(1, "DePaul", 1);
            
            Game game = new Game();
            game.AddTeam(winner);
            var result = game.AddTeam(winner);
            Assert.True(result.IsLeft);
        }

        [Fact]
        public void HoopsService_GameShouldThrowErrorForExtraTeam(){
            Team winner = new Team(1, "DePaul", 1);
            Team loser = new Team(2, "Kansas", 2);
            Team extra = new Team(3, "Kentucky", 3);

            Game game = new Game();
            game.AddTeam(winner);
            game.AddTeam(loser);
            var result = game.AddTeam(extra);
            Assert.True(result.IsLeft);
        }
    }
}