namespace SportNews.Web.Data
{
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;

	public class SportNewsDbContext : IdentityDbContext
	{
		public SportNewsDbContext(DbContextOptions<SportNewsDbContext> options)
			: base(options)
		{
		}
	}
}
