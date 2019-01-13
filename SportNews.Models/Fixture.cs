namespace SportNews.Models
{
	public class Fixture
	{
		public string EventKey { get; set; }
		public string EventDate { get; set; }
		public string EventTime { get; set; }
		public string HomeTeam { get; set; }
		public string HomeTeamKey { get; set; }
		public string AwayTeam { get; set; }
		public string AwayTeamKey { get; set; }
		public object HalftTimeResult { get; set; }
		public string FinalResult { get; set; }
		public string Status { get; set; }
		public string CountryName { get; set; }
		public string LeagueName { get; set; }
		public string LeagueKey { get; set; }
		public string LeagueRound { get; set; }
		public string LeagueSeason { get; set; }
		public string EventLive { get; set; }
		public Player[] Goalscorers { get; set; }
	}
}
