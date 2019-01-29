namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using System;
	using System.ComponentModel;

	public class PostViewModel
	{
		public int ID { get; set; }
		[DisplayName("Заглавие")]
		public string Title { get; set; }
		[DisplayName("Съдържание")]
		public string Content { get; set; }
		[DisplayName("Дата")]
		public DateTime? CreatedOn { get; set; }
		public string AuthorID { get; set; }
		public User Author { get; set; }
		[DisplayName("Отбор")]
		public int TeamID { get; set; }
		public Team Team { get; set; }
	}
}
