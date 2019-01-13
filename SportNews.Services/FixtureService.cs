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

	public class FixtureService : IFixtureService
	{
		private const string apikey = "fb70e046af276a76545b487dd030f1c964d23ddc0dfe9ee18d0399c20222dd51";
		private readonly SportNewsDbContext context;

		public FixtureService(SportNewsDbContext context)
		{
			this.context = context;
		}

		public JsonFixture AddFixture(DateTime date)
		{
			var date1 = date.ToString("yyyy-MM-dd");
			var url = $"https://allsportsapi.com/api/football/?met=Fixtures&APIkey={apikey}&from={date1}&to={date1}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonFixture>(json);

			return result;
		}

		public JsonFixture AddFixture(DateTime date, string leagueId)
		{
			var date1 = date.ToString("yyyy-MM-dd");
			var url = $"https://allsportsapi.com/api/football/?met=Fixtures&leagueId={leagueId}&APIkey={apikey}&from={date1}&to={date1}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonFixture>(json);

			return result;
		}
		
		public JsonFixture Livescore(string leagueId)
		{
			var url = $"https://allsportsapi.com/api/football/?met=Livescore&leagueId={leagueId}&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonFixture>(json);

			return result;
		}
	}
}
