namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System.Collections.Generic;

	public interface IForumService
	{
		void AddTopic(Topic topic);
		void UpdateTopic(Topic topic);
		void DeleteTopic(Topic topic);
		bool TopicExists(int id);
		Topic GetTopicByID(int? id);
		List<Topic> GetTopics();
		List<Post> GetPosts();
	}
}
