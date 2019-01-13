namespace SportNews.Models.JsonModels
{
	public class JsonCountry
	{
		public int success { get; set; }

		public Country[] result { get; set; }

		public class Country
		{
			public string country_key { get; set; }
			public string country_name { get; set; }
		}
	}
}
