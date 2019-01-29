namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using System.Collections.Generic;

	public class AllFixturesViewModel
	{
		public string LeagueName { get; set; }
		public string LeagueRound { get; set; }
		public IEnumerable<FixtureViewModel> Fixtures { get; set; }
		public IEnumerable<League> Leagues { get; set; }
		public IEnumerable<Country> Countries { get; set; }
	}
}
