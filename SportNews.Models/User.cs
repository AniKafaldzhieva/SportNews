namespace SportNews.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	
	public class User : IdentityUser
	{
		public User() : base() { }
		
		public byte[] Avatar { get; set; }
		public ICollection<Reply> Replies { get; set; } = new List<Reply>();
		public ICollection<News> News { get; set; } = new List<News>();
	}
}
