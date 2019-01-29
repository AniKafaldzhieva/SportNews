namespace SportNews.Models
{
	public class Fixture
	{
		public int ID { get; set; }
		public string EventKey { get; set; }
		public string EventDate { get; set; }
		public string EventTime { get; set; }
		public int HomeTeamID { get; set; }
		public Team HomeTeam { get; set; }
		public string HomeTeamKey { get; set; }
		public int AwayTeamID { get; set; }
		public Team AwayTeam { get; set; }
		public string AwayTeamKey { get; set; }
		public string HalftTimeResult { get; set; }
		public string FinalResult { get; set; }
		public string Status { get; set; }
		public string CountryName { get; set; }
		public string LeagueKey { get; set; }
		public string LeagueRound { get; set; }
		public string LeagueSeason { get; set; }
		//public Player[] Goalscorers { get; set; }
	}
}
