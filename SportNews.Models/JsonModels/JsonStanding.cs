namespace SportNews.Models.JsonModels
{
	public class Root
	{
		public int success { get; set; }
		public Result result { get; set; }
	}

	public class Result
	{
		public JsonStanding[] total { get; set; }
	}

	public class JsonStanding
	{
		public string standing_place { get; set; }
		public string standing_place_type { get; set; }
		public string standing_team { get; set; }
		public string standing_P { get; set; }
		public string standing_W { get; set; }
		public string standing_D { get; set; }
		public string standing_L { get; set; }
		public string standing_F { get; set; }
		public string standing_A { get; set; }
		public string standing_GD { get; set; }
		public string standing_PTS { get; set; }
		public string team_key { get; set; }
		public string league_key { get; set; }
		public string league_season { get; set; }
		public string league_round { get; set; }
		public string standing_updated { get; set; }
	}
}
