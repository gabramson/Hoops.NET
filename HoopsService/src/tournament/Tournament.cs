using HoopsService.common;
using LanguageExt;
using LanguageExt.Common;

namespace HoopsService.src.tournament
{
    public class Tournament
    {
        private Game[] games = new Game[63];
        private Field? field;
        public Tournament(Field field)
        {
            this.field = field;
            for (int i = 0; i < 63; i++)
            {
                games[i] = new Game(i+1);
            }
            for (int i = 0; i < 32; i++)
            {
                ConfigureFirstRoundGame(i);
            }
        }

        private void ConfigureFirstRoundGame(int gameId)
        {
            if (field == null){
                throw new System.Exception("Field is null.");
            }
            int[] gameFavorites = [1, 8, 5, 4, 6, 3, 7, 2];
            int favoriteId = gameFavorites[gameId % 8];
            int underdogId = 17 - favoriteId;
            favoriteId += 16 * (gameId / 8);
            underdogId += 16 * (gameId / 8);
            Option<Team> favoriteTeam = field.GetTeamById(favoriteId);
            Option<Team> underdogTeam = field.GetTeamById(underdogId);
            favoriteTeam.Match(
                team => games[gameId].AddTeam(team),
                 () => { throw new System.Exception("Favorite team not found."); }
            );
            underdogTeam.Match(
                team => games[gameId].AddTeam(team),
                 () => { throw new System.Exception("Underdog team not found."); }
            );
        }

        public List<Game> GetPlayableGames(){
            return (List<Game>)games.Filter(g => g.IsPlayable()).ToList();
        }
    }
}

