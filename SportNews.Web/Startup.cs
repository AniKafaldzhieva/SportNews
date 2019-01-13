namespace SportNews.Web
{
	using AutoMapper;
	using SportNews.Web.Data;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using SportNews.Models;
	using Microsoft.AspNetCore.Identity.UI.Services;
	using SportNews.Web.Areas.Identity.Services;
	using SportNews.Web.Infrastructure.Mapping;
	using SportNews.Services;
	using SportNews.Services.Interfaces;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			Mapper.Initialize(cfg => cfg.AddProfile<MappingProfiles>());
			services.AddAutoMapper();

			services.AddDbContext<SportNewsDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			//services.AddDefaultIdentity<IdentityUser>()
			//	.AddEntityFrameworkStores<SportNewsDbContext>();

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<SportNewsDbContext>()
				.AddDefaultUI()
				.AddDefaultTokenProviders();

			services.AddSingleton<IEmailSender, EmailSender>();
			services.Configure<AuthMessageSenderOptions>(Configuration);

			services.AddTransient<IStandingService, StandingService>();
			services.AddTransient<IFixtureService, FixtureService>();
			services.AddTransient<INewsService, NewsService>();
			services.AddTransient<IFootballService, FootbalService>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, AutoMapper.IConfigurationProvider autoMapper)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/News/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();
			app.UseHttpsRedirection();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=News}/{action=Index}/{id?}");
			});

			//autoMapper.AssertConfigurationIsValid();
		}
	}
}
