namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using System.Collections.Generic;

	public class FixtureViewModel
	{
		public string LeagueName { get; set; }
		public string LeagueRound { get; set; }
		public IEnumerable<Fixture> Fixtures { get; set; }
		public IEnumerable<League> Leagues { get; set; }
		public IEnumerable<Country> Countries { get; set; }
	}
	public class Fixture
	{
		public string EventTime { get; set; }
		public string EventResult { get; set; }
		public string EventStatus { get; set; }
		public string LeagueName { get; set; }
		public string LeagueRound { get; set; }
		public Team HomeTeam { get; set; }
		public Team AwayTeam { get; set; }
	}
}
