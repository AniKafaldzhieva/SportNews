namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using System.Collections.Generic;

	public class PostUserViewModel
	{
		public int TeamID { get; set; }
		public Team Team { get; set; }
		public IEnumerable<Post> Posts { get; set; }

		public User User { get; set; }
	}
}
