namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	public class FixtureViewModel
	{
		public string EventKey { get; set; }
		public string EventTime { get; set; }
		public string EventDate { get; set; }
		public string EventResult { get; set; }
		public string EventStatus { get; set; }
		public string LeagueName { get; set; }
		public string LeagueKey { get; set; }
		public string LeagueRound { get; set; }
		public string LeagueSeason { get; set; }
		public string HomeTeamKey { get; set; }
		public Team HomeTeam { get; set; }
		public string AwayTeamKey { get; set; }
		public Team AwayTeam { get; set; }
	}
}
