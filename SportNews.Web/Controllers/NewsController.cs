namespace SportNews.Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;
	using AutoMapper;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Models.Enums;
	using SportNews.Services;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;
	using SportNews.Web.ViewModels;

	public class NewsController : Controller
    {
        private readonly SportNewsDbContext _context;
        private readonly IMapper _mapper;
		private readonly IStandingService _service;
		private readonly INewsService newsService;
		private readonly UserManager<User> userManager;

        public NewsController(SportNewsDbContext context, IMapper mapper, IStandingService service, INewsService newsService, UserManager<User> userManager)
        {
            _context = context;
			_mapper = mapper;
			_service = service;
			this.newsService = newsService;
			this.userManager = userManager;
        }

		// GET: News
		public IActionResult Index()
		{
			AllNewsViewModel newsViewModel = new AllNewsViewModel();

			List<News> allNews = newsService.GetAllNews();
			newsViewModel.TopNews = allNews.FirstOrDefault();
			newsViewModel.FirstNews = allNews.ElementAtOrDefault(1);
			newsViewModel.SecondNews = allNews.ElementAtOrDefault(2);
			newsViewModel.SliderNews = allNews.GetRange(4, 3);
			newsViewModel.News = allNews.GetRange(7, 4);
			newsViewModel.LastNews = allNews.Skip(10);
			newsViewModel.AllNews = allNews;
			newsViewModel.Standing = _service.GetStanding(468);

			return View(newsViewModel);
		}

		public IActionResult Category(string name)
		{
			AllNewsViewModel newsViewModel = new AllNewsViewModel()
			{
				SelectedCategory = name,
				FirstNewsByCategory = newsService.GetNewsByCategory(name).FirstOrDefault(),
				NewsByCategory = newsService.GetNewsByCategory(name).Skip(1),
				AllNews = newsService.GetAllNews()
			};
			
			return View(newsViewModel);
		}

		// GET: News/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			News news = newsService.FindNewsByID(id);

			if (news == null)
			{
				return NotFound();
			}

			var model = new NewsDetailsViewModel();
			NewsDetailsViewModel newsDetailsViewModel = Mapper.Map<NewsDetailsViewModel>(news);
			newsDetailsViewModel.SuggestedNews = newsService.GetNewsByCategory(news.Category.ToString()).Where(n => n.ID != news.ID);

			return View(newsDetailsViewModel);
		}

		// GET: News/Create
		public IActionResult Create()
        {
			ViewData["Team"] = new SelectList(_context.Teams, "ID", "Name");
			return View();
        }

		// POST: News/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(NewsViewModel newsViewModel, [FromForm] IFormFile Imageinput)
		{
			if (ModelState.IsValid)
			{
				var files = HttpContext.Request.Form.Files;

				if (Imageinput != null)
				{
					if (Imageinput.Length > 0)
					{
						byte[] imageByte = null;
						using (var readStream = Imageinput.OpenReadStream())
						using (var memoryStream = new MemoryStream())
						{
							readStream.CopyTo(memoryStream);
							imageByte = memoryStream.ToArray();
						}
						newsViewModel.Image = imageByte;
					}
				}

				newsViewModel.AuthorID = newsService.GetLoggedUserID();
				News news = Mapper.Map<News>(newsViewModel);
				newsService.AddNews(news);

				ViewData["Team"] = new SelectList(_context.Teams, "Name", "Name");

				return RedirectToAction(nameof(Index));
			}

			return View(newsViewModel);
		}

		// GET: News/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var news = newsService.FindNewsByID(id);

			if (news == null)
			{
				return NotFound();
			}

			ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Name", news.TeamID);

			NewsViewModel newsViewModel = Mapper.Map<NewsViewModel>(news);
			return View(newsViewModel);
		}

		// POST: News/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("ID,Title,Content,CreatedOn,Category,Image,AuthorID,TeamID")] NewsViewModel newsViewModel, [FromForm] IFormFile Imageinput)
		{
			if (id != newsViewModel.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var files = HttpContext.Request.Form.Files;

					if (Imageinput != null)
					{
						if (Imageinput.Length > 0)
						{
							byte[] imageByte = null;
							using (var readStream = Imageinput.OpenReadStream())
							using (var memoryStream = new MemoryStream())
							{
								readStream.CopyTo(memoryStream);
								imageByte = memoryStream.ToArray();
							}
							newsViewModel.Image = imageByte;
						}
					}
					News news = Mapper.Map<News>(newsViewModel);
					news.AuthorID = newsService.GetLoggedUserID();
					newsService.UpdateNews(news);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!newsService.NewsExists(newsViewModel.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "Name", newsViewModel.TeamID);

			return View(newsViewModel);
		}

		// GET: News/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var news = newsService.FindNewsByID(id);

			if (news == null)
			{
				return NotFound();
			}

			NewsViewModel newsViewModel = Mapper.Map<NewsViewModel>(news);
			return View(newsViewModel);
		}

		// POST: News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var news = newsService.FindNewsByID(id);
			newsService.DeleteNews(news);

			return RedirectToAction(nameof(Index));
		}
	}
}
