namespace SportNews.Web.Controllers
{
	using AutoMapper;
	using Microsoft.AspNetCore.Mvc;
	using SportNews.Models;
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
			AllFixturesViewModel fixtureViewModel = new AllFixturesViewModel();
			IList<FixtureViewModel> fixtures = new List<FixtureViewModel>();
			var allFixtures = fixtureService.AddFixture(DateTime.Now.Date);

			if (id == null && allFixtures.result != null)
			{
				id = footballService
					.GetLeagueByID(allFixtures.result.FirstOrDefault(l => footballService.FindLeagueByID(l.league_key)).league_key)
					.LeagueID
					.ToString();
			}
			
			fixtureService.AddFixture(DateTime.Now.Date, id);
			List<Fixture> fixtureData = footballService.GetFixture(DateTime.Now.Date, id);

			foreach (var f in fixtureData)
			{
				fixtureService.Livescore(id);

				if (footballService.GetTeamByKey(f.HomeTeamKey) == null
					|| footballService.GetTeamByKey(f.AwayTeamKey) == null)
				{
					standingService.AddTeams(int.Parse(id));
					standingService.AddPlayers(int.Parse(id));
				}

				FixtureViewModel currentFixture = Mapper.Map<FixtureViewModel>(f);
				currentFixture.EventStatus = f.Status == null ? "0 - 0" : f.Status;

				fixtures.Add(currentFixture);
			}
			
			fixtureViewModel.LeagueName = footballService.GetLeagueByID(id).Name;
			fixtureViewModel.Countries = footballService.GetAllCountries();
			fixtureViewModel.Leagues = footballService.GetAllLeagues();
			fixtureViewModel.Fixtures = fixtures.Where(f => f.HomeTeam.Badge != null && f.AwayTeam.Badge != null);

			return View(fixtureViewModel);
		}
	}
}
