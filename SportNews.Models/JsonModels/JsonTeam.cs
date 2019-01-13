namespace SportNews.Models.JsonModels
{
	public class JsonTeam
	{
		public int success { get; set; }
		public TeamDTO[] result { get; set; }

		public class TeamDTO
		{
			public string team_key { get; set; }
			public string team_name { get; set; }
			public string team_logo { get; set; }
			public Player[] players { get; set; }
		}
		public class Player
		{
			public long player_key { get; set; }
			public string player_name { get; set; }
			public string player_number { get; set; }
			public string player_country { get; set; }
			public string player_type { get; set; }
			public string player_age { get; set; }
			public string player_match_played { get; set; }
			public string player_goals { get; set; }
			public string player_yellow_cards { get; set; }
			public string player_red_cards { get; set; }
		}
	}
}

