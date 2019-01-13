namespace SportNews.Web.ViewModels
{
	using Microsoft.AspNetCore.Mvc.Rendering;
	using SportNews.Models;
	using SportNews.Models.Enums;
	using SportNews.Models.JsonModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class AllNewsViewModel
	{
		public AllNewsViewModel()
		{
			Categories = Enum.GetNames(typeof(Categories));
		}
		public News TopNews { get; set; }
		public News FirstNews { get; set; }
		public News SecondNews { get; set; }
		public News FirstNewsByCategory { get; set; }
		public string SelectedCategory { get; set; }
		public IEnumerable<News> News { get; set; }
		public IEnumerable<News> SliderNews { get; set; }
		public IEnumerable<News> LastNews { get; set; }
		public IEnumerable<News> AllNews { get; set; }
		public IEnumerable<News> NewsByCategory { get; set; }
		public IEnumerable<string> Categories { get; set; }
		public IEnumerable<JsonStanding> Standing { get; set; }
	}
}
