namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System.Collections.Generic;

	public interface IAdminService
	{
		List<User> GetUsers();
	}
}
