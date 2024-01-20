using System.Collections.Generic;
using LanguageExt;


namespace HoopsService.src.tournament
{
    public sealed class Game
    {
        private System.Collections.Generic.HashSet<Team> teamSet = new System.Collections.Generic.HashSet<Team>();

        public Game() { }

        public Either<Exception, Team> AddTeam(Team team)
        {
            if (teamSet.Contains(team))
            {
                return new ArgumentException($"Team with Id {team.Id} is already in the game.", nameof(team));
            }
            if (teamSet.Count >= 2) {
                return new ArgumentException($"Game already has 2 teams.");
            }
            teamSet.Add(team);
            return (team);
        }

        public List<Team> GetTeams() => teamSet.ToList();
        public Either<Exception, Team> RecordWinner(Team winner){
            if (Winner.IsSome) {
                return new ArgumentException($"Winner has already been set.");
            };
            if (!teamSet.Contains(winner)) {
                return new ArgumentException($"Winner is not in the game.");
            };
            if (teamSet.Count!= 2) {
                return new ArgumentException($"Game must have 2 teams to record a winner.");
            };
            Winner = winner;
            return (winner);
        }

        public Either<Exception, Game> UnsetWinner() {
            if (Winner.IsNone) {
                return new ArgumentException($"Winner has not been set.");
            };
            Winner = Option<Team>.None;
            return (this);
        }
        public bool HasWinner() => Winner.IsSome;
        public Option<Team> Winner { get; private set; }
    }
}
