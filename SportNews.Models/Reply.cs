namespace SportNews.Models
{
	using System;
	public class Reply
	{
		public int ID { get; set; }
		public string Content { get; set; }
		public DateTime CreatedOn { get; set; }
		public int PostID { get; set; }
		public Post Post { get; set; }
		public string AuthorID { get; set; }
		public User Author { get; set; }

	}
}
