using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HoopsService.src.tournament;

namespace Hoops.Net.Pages
{
    public class RecordWinnerModel : PageModel
    {
        public List<Game> PlayableGames { get; private set; } = new List<Game>();
        public void OnGet()
        {
            var field = new Field();
            using (var reader = new StreamReader(@".\collateral\teams.csv"))
            {
                // Skip header row
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var id = int.Parse(values[0]);
                    var name = values[1].Replace("\"", "");
                    var seed = int.Parse(values[2]);

                    var team = new Team(id, name, seed);

                    field.AddTeam(team);
                }
            }
            var tournament = new Tournament(field);
            PlayableGames = tournament.GetPlayableGames();
        }
    }
}
