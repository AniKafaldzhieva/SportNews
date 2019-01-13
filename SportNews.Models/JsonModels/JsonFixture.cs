namespace SportNews.Models.JsonModels
{
	public class JsonFixture
	{
		public int success { get; set; }
		public Result[] result { get; set; }


		public class Result
		{
			public string event_key { get; set; }
			public string event_date { get; set; }
			public string event_time { get; set; }
			public string event_home_team { get; set; }
			public string home_team_key { get; set; }
			public string event_away_team { get; set; }
			public string away_team_key { get; set; }
			public string event_halftime_result { get; set; }
			public string event_final_result { get; set; }
			public string event_status { get; set; }
			public string country_name { get; set; }
			public string league_name { get; set; }
			public string league_key { get; set; }
			public string league_round { get; set; }
			public string league_season { get; set; }
			public string event_live { get; set; }
			public Goalscorer[] goalscorers { get; set; }
			public Card[] cards { get; set; }
			public Lineups lineups { get; set; }
			public Statistic[] statistics { get; set; }
		}

		public class Lineups
		{
			public Home_Team home_team { get; set; }
			public Away_Team away_team { get; set; }
		}

		public class Home_Team
		{
			public Starting_Lineups[] starting_lineups { get; set; }
			public Substitute[] substitutes { get; set; }
			public Coach[] coaches { get; set; }
		}

		public class Starting_Lineups
		{
			public string player { get; set; }
			public string player_number { get; set; }
			public string player_country { get; set; }
		}

		public class Substitute
		{
			public string player { get; set; }
			public string player_number { get; set; }
			public string player_country { get; set; }
		}

		public class Coach
		{
			public string coache { get; set; }
			public string coache_country { get; set; }
		}

		public class Away_Team
		{
			public Starting_Lineups1[] starting_lineups { get; set; }
			public Substitute1[] substitutes { get; set; }
			public Coach1[] coaches { get; set; }
		}

		public class Starting_Lineups1
		{
			public string player { get; set; }
			public string player_number { get; set; }
			public string player_country { get; set; }
		}

		public class Substitute1
		{
			public string player { get; set; }
			public string player_number { get; set; }
			public string player_country { get; set; }
		}

		public class Coach1
		{
			public string coache { get; set; }
			public string coache_country { get; set; }
		}

		public class Goalscorer
		{
			public string time { get; set; }
			public string home_scorer { get; set; }
			public string score { get; set; }
			public string away_scorer { get; set; }
		}

		public class Card
		{
			public string time { get; set; }
			public string home_fault { get; set; }
			public string card { get; set; }
			public string away_fault { get; set; }
		}

		public class Statistic
		{
			public string type { get; set; }
			public string home { get; set; }
			public string away { get; set; }
		}
	}
}
	
