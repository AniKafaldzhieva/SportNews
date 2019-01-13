namespace SportNews.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SportNews.Services.Interfaces;
	using SportNews.Web.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class FixtureController : Controller
	{
		private readonly IFixtureService fixtureService;
		private readonly IStandingService standingService;
		private readonly IFootballService footballService;

		public FixtureController(IFixtureService fixtureService, IStandingService standingService, IFootballService footballService)
		{
			this.fixtureService = fixtureService;
			this.standingService = standingService;
			this.footballService = footballService;
		}
		public IActionResult Index(string id)
		{
			FixtureViewModel fixtureViewModel = new FixtureViewModel();
			IList<Fixture> fixtures = new List<Fixture>();
			var allFixtures = fixtureService.AddFixture(DateTime.Now.Date);

			if (id == null && allFixtures.result != null)
			{
				id = footballService.GetLeagueByID(allFixtures.result.FirstOrDefault().league_key).LeagueID.ToString();
			}

			var fixtureData = fixtureService.AddFixture(DateTime.Now.Date, id);

			if (fixtureData.success == 1 && fixtureData.result != null)
			{
				foreach (var f in fixtureData.result.Where(x => x.league_key == id))
				{
					fixtureService.Livescore(id);

					if (footballService.GetTeamByKey(f.home_team_key) == null
						|| footballService.GetTeamByKey(f.away_team_key) == null)
					{
						standingService.AddTeams(int.Parse(id));
						standingService.AddPlayers(int.Parse(id));
					}
					var currentFixture = new Fixture()
					{
						EventTime = f.event_time,
						LeagueName = f.league_name,
						LeagueRound = f.league_round,
						EventResult = f.event_final_result,
						EventStatus = f.event_status == " " ? "0 - 0" : f.event_status,
						HomeTeam = footballService.GetTeamByKey(f.home_team_key),
						AwayTeam = footballService.GetTeamByKey(f.away_team_key)
					};

					fixtures.Add(currentFixture);
				}
			}

			fixtureViewModel.LeagueName = footballService.GetLeagueByID(id).Name;
			fixtureViewModel.Countries = footballService.GetAllCountries();
			fixtureViewModel.Leagues = footballService.GetAllLeagues();
			fixtureViewModel.Fixtures = fixtures.Where(f => f.HomeTeam.Badge != null && f.AwayTeam.Badge != null);

			return View(fixtureViewModel);
		}
	}
}
