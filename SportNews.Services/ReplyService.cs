namespace SportNews.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;

	public class ReplyService : IReplyService
	{
		private readonly SportNewsDbContext context;

		public ReplyService(SportNewsDbContext context)
		{
			this.context = context;
		}

		public List<Reply> GetRepliesByPostID(int postID)
		{
			return context.Replies
				.Include(r => r.Author)
				.Include(r => r.Post)
				.Where(p => p.PostID == postID)
				.ToList();
		}
	}
}
