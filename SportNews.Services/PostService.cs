namespace SportNews.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;

	public class PostService : IPostService
	{
		private readonly SportNewsDbContext context;

		public PostService(SportNewsDbContext context)
		{
			this.context = context;
		}

		public void AddPost(Post post)
		{
			context.Posts.Add(post);
			context.SaveChanges();
		}

		public Post FindPostByID(int? id)
		{
			return context.Posts.Find(id);
		}

		public List<Post> GetPostsByTeamID(int teamID)
		{
			return context
				.Posts
				.Include(p => p.Author)
				.Include(p => p.Team)
				.Where(p => p.TeamID == teamID)
				.ToList();
		}

		public Team GetTeamByID(int teamID)
		{
			return context
				.Teams
				.FirstOrDefault(t => t.ID == teamID);
		}

		public void UpdatePost(Post post)
		{
			context.Update(post);
			context.SaveChanges();
		}

		public bool PostExists(int id)
		{
			return context.Posts.Any(e => e.ID == id);
		}

		public void DeletePost(Post post)
		{
			context.Posts.Remove(post);
			context.SaveChanges();
		}

		public Post GetPostByID(int? id)
		{
			return context
				.Posts
				.Include(p => p.Author)
				.FirstOrDefault(p => p.ID == id);
		}
	}
}
