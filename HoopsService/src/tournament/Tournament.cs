using HoopsService.common;
using LanguageExt;
using LanguageExt.Common;

namespace HoopsService.src.tournament
{
    public record Event(Game Game, Team Winner);
    public class Tournament
    {
        private List<Event> events = new List<Event>();
        private Game[] games = new Game[63];
        private Field? field;
        public Tournament(Field field)
        {
            this.field = field;
            for (int i = 0; i < 63; i++)
            {
                games[i] = new Game(i + 1);
            }
            for (int i = 0; i < 32; i++)
            {
                ConfigureFirstRoundGame(i);
            }
        }

        private void ConfigureFirstRoundGame(int gameSlot)
        {
            if (field == null)
            {
                throw new System.Exception("Field is null.");
            }
            int[] gameFavorites = [1, 8, 5, 4, 6, 3, 7, 2];
            int favoriteId = gameFavorites[gameSlot % 8];
            int underdogId = 17 - favoriteId;
            favoriteId += 16 * (gameSlot / 8);
            underdogId += 16 * (gameSlot / 8);
            Option<Team> favoriteTeam = field.GetTeamById(favoriteId);
            Option<Team> underdogTeam = field.GetTeamById(underdogId);
            favoriteTeam.Match(
                team => games[gameSlot].AddTeam(team),
                 () => { throw new System.Exception("Favorite team not found."); }
            );
            underdogTeam.Match(
                team => games[gameSlot].AddTeam(team),
                 () => { throw new System.Exception("Underdog team not found."); }
            );
        }

        private int NextGameSlot(int currentGameSlot)
        {
            return 63 - (63 - currentGameSlot) / 2;
        }

        public List<Game> GetPlayableGames()
        {
            return (List<Game>)games.Filter(g => g.IsPlayable()).ToList();
        }

        public Fin<Success> RecordWinner(Game game, Team winner)
        {
            events.Add(new Event(game, winner));
            game.RecordWinner(winner);
            games[NextGameSlot(game.id - 1)].AddTeam(winner);
            return new Success();
        }

        public IEnumerable<Event> GetMostRecentEvents()
        {
            return events.AsEnumerable().Reverse();
        }

        public Fin<Success> UndoMostRecentEvent(){
            if (events.Count == 0)
            {
                return Error.New("No events to undo.");
            }
            Event lastEvent = events.Last();
            Game game = lastEvent.Game;
            int gameSlot = game.id - 1;
            games[NextGameSlot(gameSlot)].RemoveTeam(lastEvent.Winner);
            game.UnsetWinner();
            events.Remove(lastEvent);
            return new Success();
        }
    }
}


