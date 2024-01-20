using HoopsService.common;
using LanguageExt;
using LanguageExt.Common;

namespace HoopsService.src.tournament
{
    public class Field
    {
        private readonly List<Team> teams;

        public Field()
        {
            teams = new List<Team>();
        }

        public Either<Error, Success> AddTeam(Team team)
        {
            if (teams.Any(t => t.Id == team.Id))
            {
                return Error.New($"Team with Id {team.Id} already exists in the field.", nameof(team));
            }

            teams.Add(team);
            return new Success();
        }

        public Option<Team> GetTeamById(int teamId)
        {
            var found = teams.Find(t => t.Id == teamId);
            if (found != null) { return Option<Team>.Some(found); }
            return Option<Team>.None;
        }

        public Option<Team> GetTeamByName(string teamName)
        {
            var found = teams.Find(t => t.Name == teamName);
            if (found != null) { return Option<Team>.Some(found); }
            return Option<Team>.None;
        }
    }
}