namespace SportNews.Models
{
	using SportNews.Models.Enums;
	using System.Collections.Generic;

	public class Team
	{
		public int ID { get; set; }
		public int TeamKey { get; set; }
		public string Name { get; set; }
		public byte[] Badge { get; set; }
		public Categories Category { get; set; }
		public int LeagueId { get; set; }
		public League League { get; set; }
		public ICollection<Player> Players { get; set; } = new List<Player>();
	}
}
