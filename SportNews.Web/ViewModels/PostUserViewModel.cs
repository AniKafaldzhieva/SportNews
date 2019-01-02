namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using System.Collections.Generic;

	public class PostUserViewModel
	{
		public IEnumerable<Post> Posts { get; set; }

		public User User { get; set; }
	}
}
