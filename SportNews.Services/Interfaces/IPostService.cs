namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System.Collections.Generic;

	public interface IPostService
	{
		void AddPost(Post post);
		void UpdatePost(Post post);
		bool PostExists(int id);
		void DeletePost(Post post);
		Post FindPostByID(int? id);
		Post GetPostByID(int? id);
		Team GetTeamByID(int teamID);
		List<Post> GetPostsByTeamID(int teamID);
	}
}
