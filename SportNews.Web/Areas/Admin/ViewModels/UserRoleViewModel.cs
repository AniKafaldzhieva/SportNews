namespace SportNews.Web.Areas.Admin.ViewModels
{
	using SportNews.Models;
	using System.Collections.Generic;

	public class UserRoleViewModel
	{
		public IEnumerable<User> Users { get; set; }

		public IEnumerable<Role> Roles { get; set; }
	}
}
