namespace SportNews.Models
{
	public class Player
	{
		public int ID { get; set; }
		public string PlayerKey { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string Type { get; set; }
		public string Country { get; set; }
		public int TeamID { get; set; }
		public Team Team { get; set; }
		public int Age { get; set; }
		public int MatchPlayed { get; set; }
		public int Goals { get; set; }
		public int YellowCards { get; set; }
		public int RedCards { get; set; }
	}
}
