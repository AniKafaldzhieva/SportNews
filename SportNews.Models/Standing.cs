using System.ComponentModel.DataAnnotations.Schema;

namespace SportNews.Models
{
	public class Standing
	{
		public int ID { get; set; }
		public int LeagueId { get; set; }
		public League League { get; set; }
		public string LeagueKey { get; set; }
		public string LeagueSeason { get; set; }
		public string LeagueRound { get; set; }
		public string Place { get; set; }
		public string PlaceType { get; set; }
		public int TeamID { get; set; }
		public Team Team { get; set; }
		public string TeamKey { get; set; }
		public string GamesPlayed { get; set; }
		public string Wins { get; set; }
		public string Draw { get; set; }
		public string Losts { get; set; }
		public string GoalsFor { get; set; }
		public string GoalsAgainst { get; set; }
		public string GoalDifference { get; set; }
		public string Points { get; set; }
		public string StandingUpdated { get; set; }
	}
}
