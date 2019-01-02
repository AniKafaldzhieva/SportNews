namespace SportNews.Models
{
	using System.Collections.Generic;
	public class League
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public IList<Team> Teams { get; set; } = new List<Team>();
	}
}
