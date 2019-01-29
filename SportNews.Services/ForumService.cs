namespace SportNews.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;

	public class ForumService : IForumService
	{
		private readonly SportNewsDbContext context;

		public ForumService(SportNewsDbContext context)
		{
			this.context = context;
		}

		public void AddTopic(Topic topic)
		{
			context.Add(topic);
			context.SaveChanges();
		}

		public void DeleteTopic(Topic topic)
		{
			context.Topics.Remove(topic);
			context.SaveChanges();
		}

		public List<Post> GetPosts()
		{
			return context
				.Posts
				.Include(p => p.Author)
				.Include(p => p.Team)
				.ToList();
		}

		public Topic GetTopicByID(int? id)
		{
			return context.Topics.Find(id);
		}

		public List<Topic> GetTopics()
		{
			return context.Topics.Include(t => t.Team).ToList();
		}

		public bool TopicExists(int id)
		{
			return context.Topics.Any(e => e.ID == id);
		}

		public void UpdateTopic(Topic topic)
		{
			context.Update(topic);
			context.SaveChanges();
		}
	}
}
