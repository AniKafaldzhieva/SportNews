namespace SportNews.Models.JsonModels
{
	public class JsonLeagues
	{
		public int success { get; set; }
		public League[]	result { get; set; }

		public class League
		{
			public string country_key { get; set; }
			public string country_name { get; set; }
			public string league_key { get; set; }
			public string league_name { get; set; }
		}
	}
}
