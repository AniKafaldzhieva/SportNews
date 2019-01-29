namespace SportNews.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Areas.Admin.ViewModels;
	using SportNews.Web.Infrastructure.Constraints;
	using System.Linq;
	using System.Threading.Tasks;

	[Area("Admin")]
	[Authorize(Roles = RoleConstraints.Administrator)]
	public class AdminController : Controller
    {
		public readonly IAdminService adminService;
		public readonly RoleManager<Role> roleManager;
		public readonly UserManager<User> userManager;

		public AdminController(IAdminService adminService, RoleManager<Role> roleManager, UserManager<User> userManager)
		{
			this.adminService = adminService;
			this.roleManager = roleManager;
			this.userManager = userManager;
		}

        public IActionResult Index()
        {

			UserRoleViewModel userRoleViewModel = new UserRoleViewModel();
			userRoleViewModel.Users = adminService.GetUsers();
			userRoleViewModel.Roles = roleManager.Roles.ToList();
			var a = roleManager
				.Roles
				.Select(r => new SelectListItem
					{
						Text = r.Name,
						Value = r.Name
					})
				.ToList();
			ViewData["Role"] = new SelectList(roleManager
				.Roles
				.Select(r => new SelectListItem
				{
					Text = r.Name,
					Value = r.Name
				}), "Name", "Name");

			return View(userRoleViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> AddToRoleAsync(AddUserToRoleViewModel model)
		{
			var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
			var user = await this.userManager.FindByIdAsync(model.UserId);
			var userExists = user != null;

			if (!roleExists || !userExists)
			{
				ModelState.AddModelError(string.Empty, "Invalid identity details.");
			}

			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index));
			}

			await this.userManager.AddToRoleAsync(user, model.Role);

			TempData["SuccessfullMessage"] = ($"User {user.UserName} successfully added to the {model.Role} role.");
			return RedirectToAction(nameof(Index));
		}
	}
}