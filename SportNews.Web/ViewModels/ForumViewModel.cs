namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using SportNews.Models.Enums;
	using System;
	using System.Collections.Generic;

	public class ForumViewModel
	{
		public ForumViewModel()
		{
			Categories = Enum.GetNames(typeof(Categories));
		}
		public string[] Categories { get; set; }

		public IEnumerable<Team> Teams { get; set; }

		public User User { get; set; }
	}
}
