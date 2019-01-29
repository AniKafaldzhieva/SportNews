namespace SportNews.Web.Infrastructure.Extensions
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using SportNews.Models;
	using SportNews.Web.Data;
	using SportNews.Web.Infrastructure.Constraints;
	using System;
	using System.IO;
	using System.Threading.Tasks;

	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				serviceScope.ServiceProvider.GetService<SportNewsDbContext>().Database.Migrate();

				var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
				var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();

				Task
					.Run(async () =>
					{
						var admin = RoleConstraints.Administrator;

						var roles = new[]
						{
							admin,
							RoleConstraints.Moderator
						};

						foreach (var role in roles)
						{
							var roleExists = await roleManager.RoleExistsAsync(role);

							if (!roleExists)
							{
								await roleManager.CreateAsync(new Role
								{
									Name = role
								});
							}
						}

						var adminEmail = "admin@admin.com";

						var adminUser = await userManager.FindByEmailAsync(adminEmail);

						if (adminUser == null)
						{
							adminUser = new User
							{
								Email = adminEmail,
								UserName = adminEmail,
								SecurityStamp = Guid.NewGuid().ToString(),
								Avatar = File.ReadAllBytes("wwwroot/images/default-user.png")
						};

							await userManager.CreateAsync(adminUser, "A123123a!");

							await userManager.AddToRoleAsync(adminUser, admin);
						}
					})
					.Wait();
			}

			return app;
		}
	}
}
