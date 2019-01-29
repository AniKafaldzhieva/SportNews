namespace SportNews.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;

	public class AdminService : IAdminService
	{
		private readonly SportNewsDbContext context;

		public AdminService(SportNewsDbContext context)
		{
			this.context = context;
		}
		public List<User> GetUsers()
		{
			return context.Users.ToList();
		}
	}
}
