namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using SportNews.Models.Enums;
	using System.ComponentModel;

	public class TopicViewModel
	{
		public int ID { get; set; }
		[DisplayName("Подзаглавие")]
		public string Subtitle { get; set; }
		[DisplayName("Автор")]
		public User Author { get; set; }
		[DisplayName("Категория")]
		public Categories Category { get; set; }
		[DisplayName("Отбор")]
		public int TeamID { get; set; }
		public Team Team { get; set; }
	}
}
