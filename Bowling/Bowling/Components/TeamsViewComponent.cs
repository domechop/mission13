using System;
using System.Linq;
using Bowling.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private BowlingDbContext repo { get; set; }


        public TeamsViewComponent(BowlingDbContext temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["team"];

            var teams = repo.Bowlers
                .Select(x => x.Team.TeamName)
                .Distinct()
                .OrderBy(x => x);
            return View(teams);
        }
    }
}
