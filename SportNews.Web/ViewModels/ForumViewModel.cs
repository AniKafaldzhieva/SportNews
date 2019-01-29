namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using SportNews.Models.Enums;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class ForumViewModel
	{
		public ForumViewModel()
		{
			Categories = Enum.GetNames(typeof(Categories));
		}
		[DisplayName("Категории")]
		public string[] Categories { get; set; }
		public User User { get; set; }
		public IEnumerable<Topic> Topics { get; set; }
		public IEnumerable<Post> Posts { get; set; }
	}
}
