namespace SportNews.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;
	using SportNews.Web.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class FixtureController : Controller
	{
		private readonly IFixtureService service;
		private readonly SportNewsDbContext context;
		private readonly IStandingService standingService;

		public FixtureController(IFixtureService service, SportNewsDbContext context, IStandingService standingService)
		{
			this.service = service;
			this.context = context;
			this.standingService = standingService;
		}
		public IActionResult Index(string id)
		{
			var model = new FixtureViewModel();
			var list = new List<Fixture>();

			if (id != null)
			{
				var fixtureData = service.AddFixture(DateTime.Now.Date, id);

				foreach (var f in fixtureData.result.Where(x => x.league_key == id))
				{
					//standingService.AddTeams(int.Parse(f.league_key));
					//standingService.AddPlayers(int.Parse(f.league_key));
					service.Livescore(id);

					var currentFixture = new Fixture()
					{
						EventTime = f.event_time,
						LeagueName = f.league_name,
						LeagueRound = f.league_round,
						EventResult = f.event_final_result,
						EventStatus = f.event_status,
						HomeTeam = context.Teams.FirstOrDefault(t => t.TeamKey.ToString() == f.home_team_key),
						AwayTeam = context.Teams.FirstOrDefault(t => t.TeamKey.ToString() == f.away_team_key)
					};

					list.Add(currentFixture);
				}
				
				model.LeagueName = context.Leagues.FirstOrDefault(l => l.LeagueID.ToString() == id).Name;
			}

			model.Countries = context.Countries;
			model.Leagues = context.Leagues;
			model.Fixtures = list.Where(f => f.HomeTeam.Badge != null && f.AwayTeam.Badge != null);

			return View(model);
		}
	}
}
