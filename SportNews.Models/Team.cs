namespace SportNews.Models
{
	using SportNews.Models.Enums;
	using System.ComponentModel.DataAnnotations;

	public class Team
	{
		public int ID { get; set; }
		[UIHint("tinymce_jquery_full")]
		public string Name { get; set; }
		public byte[] Badge { get; set; }
		public Categories Category { get; set; }
		public int LeagueId { get; set; }
		public League League { get; set; }
	}
}
