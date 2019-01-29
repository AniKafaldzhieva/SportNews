namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System.Collections.Generic;

	public interface IReplyService
	{
		List<Reply> GetRepliesByPostID(int postID);
	}
}
