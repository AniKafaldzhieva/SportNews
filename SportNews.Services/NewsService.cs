namespace SportNews.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;

	public class NewsService : INewsService
	{
		private readonly SportNewsDbContext context;
		private readonly UserManager<User> userManager;
		private readonly IHttpContextAccessor accessor;
		
		public NewsService(SportNewsDbContext context, UserManager<User> userManager, IHttpContextAccessor accessor)
		{
			this.context = context;
			this.userManager = userManager;
			this.accessor = accessor;
		}

		public List<News> GetAllNews()
		{
			return context
				.News
				.Include(n => n.Author)
				.Include(n => n.Team)
				.OrderByDescending(n => n.CreatedOn)
				.ToList();
		}

		public List<News> GetNewsByCategory(string category)
		{
			return context
				.News
				.Include(n => n.Author)
				.Include(n => n.Team)
				.Where(n => n.Category.ToString() == category)
				.OrderByDescending(n => n.CreatedOn)
				.ToList();
		}

		public void AddNews(News news)
		{
			context.News.Add(news);
			context.SaveChanges();
		}

		public News FindNewsByID(int? id)
		{
			return context.News
				.Include(n => n.Author)
				.Include(n => n.Team)
				.FirstOrDefault(m => m.ID == id);
		}

		public void UpdateNews(News news)
		{
			context.Update(news);
			context.SaveChanges();
		}

		public bool NewsExists(int id)
		{
			return context.News.Any(e => e.ID == id);
		}

		public string GetLoggedUserID()
		{
			return userManager.GetUserId(accessor.HttpContext.User);
		}

		public void DeleteNews(News news)
		{
			context.News.Remove(news);
			context.SaveChanges();
		}
	}
}
