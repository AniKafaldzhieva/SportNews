namespace SportNews.Web.Data
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;

	public class SportNewsDbContext : IdentityDbContext<User, Role, string>
	{
		public SportNewsDbContext(DbContextOptions<SportNewsDbContext> options)
			: base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<League> Leagues { get; set; }
		public DbSet<Reply> Replies { get; set; }
	}
}
