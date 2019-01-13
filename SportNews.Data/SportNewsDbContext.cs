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
		public DbSet<News> News { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Standing> Standings { get; set; }
		public DbSet<Player> Players { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Country>().HasIndex(c => c.CountryID).IsUnique();
		}
	}
}
