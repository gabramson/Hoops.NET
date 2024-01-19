using System.Collections.Generic;
using LanguageExt;


namespace HoopsService.src.tournament
{
    public sealed class Game
    {
        System.Collections.Generic.HashSet<Team> teamSet = new System.Collections.Generic.HashSet<Team>();

        public Game() { }

        public Either<Exception, Team> AddTeam(Team team)
        {
            if (teamSet.Contains(team))
            {
                return new ArgumentException($"Team with Id {team.Id} is alread in the game.", nameof(team));
            }
            teamSet.Add(team);
            return (team);
        }

        public List<Team> GetTeams() => teamSet.ToList();
    }
}
