namespace SportNews.Models
{
	using SportNews.Models.Enums;
	using System;
	public class News
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? CreatedOn { get; set; }
		public Categories Category { get; set; }
		public byte[] Image { get; set; }

		public string AuthorID { get; set; }
		public User Author { get; set; }

		public int TeamID { get; set; }
		public Team Team { get; set; }
	}
}
