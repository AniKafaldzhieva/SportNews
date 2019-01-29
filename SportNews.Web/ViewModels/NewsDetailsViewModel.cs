namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using SportNews.Models.Enums;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class NewsDetailsViewModel
	{
		public int ID { get; set; }
		[DisplayName("Заглавие")]
		public string Title { get; set; }
		[DisplayName("Съдържание")]
		public string Content { get; set; }
		[DisplayName("Дата")]
		public DateTime? CreatedOn { get; set; }
		[DisplayName("Категория")]
		public Categories Category { get; set; }
		[DisplayName("Изображение")]
		public byte[] Image { get; set; }

		public string AuthorID { get; set; }
		public User Author { get; set; }
		[DisplayName("Отбор")]
		public int TeamID { get; set; }
		public Team Team { get; set; }

		public IEnumerable<News> SuggestedNews { get; set; }
	}
}
