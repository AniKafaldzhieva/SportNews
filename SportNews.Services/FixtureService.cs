namespace SportNews.Services
{
	using System;
	using System.Net;
	using Newtonsoft.Json;
	using SportNews.Web.Data;
	using SportNews.Services.Interfaces;
	using SportNews.Models.JsonModels;
	using System.Collections.Generic;
	using System.Linq;
	using SportNews.Models;
	using AutoMapper;

	public class FixtureService : IFixtureService
	{
		private const string apikey = "14fc664cf4fd746a519457ca77c1f88b33bada2fca2e0c86211b5364242fec31";
		private readonly SportNewsDbContext context;
		private readonly IStandingService service;

		public FixtureService(SportNewsDbContext context, IStandingService service)
		{
			this.context = context;
			this.service = service;
		}

		public JsonFixture AddFixture(DateTime date)
		{
			var date1 = date.ToString("yyyy-MM-dd");
			var url = $"https://allsportsapi.com/api/football/?met=Fixtures&APIkey={apikey}&from={date1}&to={date1}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonFixture>(json);

			return result;
		}

		public void AddFixture(DateTime date, string leagueId)
		{
			var parseDate = date.ToString("yyyy-MM-dd");
			var url = $"https://allsportsapi.com/api/football/?met=Fixtures&leagueId={leagueId}&APIkey={apikey}&from={parseDate}&to={parseDate}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonFixture>(json);

			if (result.success == 1 && result.result != null)
			{
				foreach (var jsonFixture in result.result)
				{
					Fixture fixture = Mapper.Map<Fixture>(jsonFixture);
					fixture.HomeTeam = context.Teams.FirstOrDefault(t => t.TeamKey.ToString() == jsonFixture.home_team_key);
					fixture.AwayTeam = context.Teams.FirstOrDefault(t => t.TeamKey.ToString() == jsonFixture.away_team_key);
					if (fixture.AwayTeam == null)
					{
						service.AddTeams(int.Parse(jsonFixture.league_key));
					}
					Fixture fixtureDB = context.Fixtures.FirstOrDefault(s => s.EventKey == jsonFixture.event_key);

					if (fixtureDB == null)
					{
						context.Fixtures.Add(fixture);
					}
					else
					{
						if (!Equals(fixture, fixtureDB))
						{
							fixture.ID = fixtureDB.ID;
							fixture.HomeTeamID = fixtureDB.HomeTeamID;
							fixture.AwayTeamID = fixtureDB.AwayTeamID;

							context.Entry(fixtureDB).CurrentValues.SetValues(fixture);
						}
					}
				}

				context.SaveChanges();
			}
		}
		
		public JsonFixture Livescore(string leagueId)
		{
			var url = $"https://allsportsapi.com/api/football/?met=Livescore&leagueId={leagueId}&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonFixture>(json);

			return result;
		}

		private bool Equals(Fixture obj, Fixture obj2)
		{
			if (obj.AwayTeamKey == obj2.AwayTeamKey
				&& obj.CountryName == obj2.CountryName
				&& obj.EventDate == obj2.EventDate
				&& obj.LeagueSeason == obj2.LeagueSeason
				&& obj.EventKey == obj2.EventKey
				&& obj.EventTime == obj2.EventTime
				&& obj.FinalResult == obj2.FinalResult
				&& obj.HalftTimeResult == obj2.HalftTimeResult
				&& obj.HomeTeamKey == obj2.HomeTeamKey
				&& obj.LeagueKey == obj2.LeagueKey
				&& obj.LeagueRound == obj2.LeagueRound
				&& obj.LeagueSeason == obj2.LeagueSeason
				&& obj.Status == obj2.Status)
				return true;

			return false;
		}
	}
}
