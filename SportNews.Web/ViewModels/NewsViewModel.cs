namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using SportNews.Models.Enums;
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	public class NewsViewModel
	{
		public int ID { get; set; }
		[DisplayName("Заглавие")]
		[Required(ErrorMessage ="Полето е задължително!")]
		public string Title { get; set; }
		[DisplayName("Съдържание")]
		[Required(ErrorMessage = "Полето е задължително!")]
		[MinLength(20, ErrorMessage = "Съдържанието трябва да бъде над 50 символа.")]
		public string Content { get; set; }
		[DisplayName("Дата")]
		[Required(ErrorMessage = "Полето е задължително!")]
		public DateTime? CreatedOn { get; set; }
		[DisplayName("Категория")]
		public Categories Category { get; set; }
		[DisplayName("Изображение")]
		[Required(ErrorMessage = "Полето е задължително!")]
		public byte[] Image { get; set; }

		public string AuthorID { get; set; }
		public User Author { get; set; }

		[DisplayName("Отбор")]
		public int TeamID { get; set; }
		public Team Team { get; set; }
	}
}
