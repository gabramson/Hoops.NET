using System.Collections.Generic;
using HoopsService.common;
using LanguageExt;
using LanguageExt.Common;


namespace HoopsService.src.tournament
{
    public sealed class Game
    {
        public int id {get;}
        private System.Collections.Generic.HashSet<Team> teamSet = new System.Collections.Generic.HashSet<Team>();

        public Game(int id) {
            this.id = id;
         }

        public Either<Error, Success> AddTeam(Team team)
        {
            if (teamSet.Contains(team))
            {
                return Error.New($"Team with Id {team.Id} is already in the game.", nameof(team));
            }
            if (teamSet.Count >= 2) {
                return Error.New($"Game already has 2 teams.");
            }
            teamSet.Add(team);
            return new Success();
        }

        public Either<Error, Success> RemoveTeam(Team team){
            if (!teamSet.Contains(team)) {
                return Error.New($"Team with Id {team.Id} is not in the game.", nameof(team));
            };
            teamSet.Remove(team);
            return new Success();
        }

        public List<Team> GetTeams() => teamSet.ToList();
        public Either<Error, Success> RecordWinner(Team winner){
            if (Winner.IsSome) {
                return Error.New($"Winner has already been set.");
            };
            if (!teamSet.Contains(winner)) {
                return Error.New($"Winner is not in the game.");
            };
            if (teamSet.Count!= 2) {
                return Error.New($"Game must have 2 teams to record a winner.");
            };
            Winner = winner;
            return new Success();
        }

        public Either<Error, Success> UnsetWinner() {
            if (Winner.IsNone) {
                return Error.New($"Winner has not been set.");
            };
            Winner = Option<Team>.None;
            return new Success();
        }
        public bool HasWinner() => Winner.IsSome;
        public bool IsPlayable() => teamSet.Count == 2 && Winner.IsNone;
        public Option<Team> Winner { get; private set; }
    }
}
