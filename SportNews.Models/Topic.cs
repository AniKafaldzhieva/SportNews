using SportNews.Models.Enums;

namespace SportNews.Models
{
	public class Topic
	{
		public int ID { get; set; }
		public string Subtitle { get; set; }
		public Categories Category { get; set; }
		public string AuthorID { get; set; }
		public User Author { get; set; }
		public int TeamID { get; set; }
		public Team Team { get; set; }
	}
}
