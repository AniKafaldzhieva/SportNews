namespace SportNews.Web.ViewModels
{
	using SportNews.Models;
	using System.Collections.Generic;

	public class ReplyViewModel
	{
		public Post Post { get; set; }
		public IList<Reply> Replies { get; set; } 
	}
}
