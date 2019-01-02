namespace SportNews.Models
{
	using System;
	using System.Collections.Generic;

	public class Post
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime? CreatedOn { get; set; }
		public string AuthorID { get; set; }
		public User Author { get; set; }

		public int TeamID { get; set; }
		public Team Team { get; set; }

		public ICollection<Reply> Replies { get; set; } = new List<Reply>();
	}
}
