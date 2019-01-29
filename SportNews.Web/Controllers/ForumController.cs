namespace SportNews.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Infrastructure.Constraints;
	using SportNews.Web.ViewModels;

	public class ForumController : Controller
    {
        private readonly IFootballService footballService;
        private readonly INewsService newsService;
        private readonly IForumService forumService;

        public ForumController(IFootballService footballService, INewsService newsService, IForumService forumService)
        {
			this.footballService = footballService;
			this.newsService = newsService;
			this.forumService = forumService;
        }

		// GET: Forum
		public IActionResult Index(string id)
		{
			ForumViewModel forum = new ForumViewModel();
			forum.Topics = forumService.GetTopics();
			forum.Posts = forumService.GetPosts();

			return View(forum);
		}

		// GET: Forum/Details/5
		//public async Task<IActionResult> Details(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return NotFound();
  //          }

  //          var team = await _context.Teams
  //              .Include(t => t.League)
  //              .FirstOrDefaultAsync(m => m.ID == id);
  //          if (team == null)
  //          {
  //              return NotFound();
  //          }

  //          return View(team);
  //      }

        // GET: Forum/Create
        public IActionResult Create()
        {
            ViewData["Team"] = new SelectList(footballService.GetAllTeams(), "ID", "Name");
            return View();
        }

		// POST: Forum/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleConstraints.Administrator + "," + RoleConstraints.Moderator)]
		public IActionResult Create(TopicViewModel topicViewModel)
		{
			if (ModelState.IsValid)
			{
				Topic topic = AutoMapper.Mapper.Map<Topic>(topicViewModel);
				topic.AuthorID = newsService.GetLoggedUserID();
				forumService.AddTopic(topic);

				return RedirectToAction(nameof(Index));
			}

			ViewData["Team"] = new SelectList(footballService.GetAllTeams(), "ID", "Name");

			return View(topicViewModel);
		}

		// GET: Forum/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var topic = forumService.GetTopicByID(id);

			if (topic == null)
			{
				return NotFound();
			}
			//ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", team.LeagueId);

			var topicViewModel = AutoMapper.Mapper.Map<TopicViewModel>(topic);

			ViewData["Team"] = new SelectList(footballService.GetAllTeams(), "ID", "Name");

			return View(topicViewModel);
		}

		// POST: Forum/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleConstraints.Administrator + "," + RoleConstraints.Moderator)]
		public IActionResult Edit(int id, TopicViewModel topicViewModel)
		{
			if (id != topicViewModel.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					Topic topic = AutoMapper.Mapper.Map<Topic>(topicViewModel);

					forumService.UpdateTopic(topic);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!forumService.TopicExists(topicViewModel.ID))
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

			return View(topicViewModel);
		}

		// GET: Forum/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var topic = forumService.GetTopicByID(id);
			if (topic == null)
			{
				return NotFound();
			}

			var topicViewModel = AutoMapper.Mapper.Map<TopicViewModel>(topic);
			return View(topicViewModel);
		}

		// POST: Forum/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleConstraints.Administrator + "," + RoleConstraints.Moderator)]
		public IActionResult DeleteConfirmed(int id)
		{
			var topic = forumService.GetTopicByID(id);
			forumService.DeleteTopic(topic);

			return RedirectToAction(nameof(Index));
		}
	}
}
