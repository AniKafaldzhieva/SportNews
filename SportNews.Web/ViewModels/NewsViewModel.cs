namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using SportNews.Models.Enums;
	using System;
	using System.ComponentModel;

	public class NewsViewModel
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

		public int TeamID { get; set; }
		[DisplayName("Отбор")]
		public Team Team { get; set; }
	}
}
