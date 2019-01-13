namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System.Collections.Generic;

	public interface INewsService
	{
		List<News> GetAllNews();
		List<News> GetNewsByCategory(string category);
		void AddNews(News news);
		News FindNewsByID(int? id);
		void UpdateNews(News news);
		bool NewsExists(int id);
		string GetLoggedUserID();
		void DeleteNews(News news);
	}
}
