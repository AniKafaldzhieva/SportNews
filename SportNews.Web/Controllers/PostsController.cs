namespace SportNews.Web.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.EntityFrameworkCore;
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;
	using SportNews.Web.Infrastructure.Constraints;
	using SportNews.Web.ViewModels;

	public class PostsController : Controller
    {
        private readonly SportNewsDbContext _context;
		private readonly UserManager<User> manager;
		private readonly IPostService postService;

        public PostsController(SportNewsDbContext context, UserManager<User> manager, IPostService postService)
        {
            _context = context;
			this.manager = manager;
			this.postService = postService;
        }

		// GET: Posts
		public IActionResult Index(int id)
		{
			PostUserViewModel postUserViewModel = new PostUserViewModel
			{
				TeamID = id,
				Team = postService.GetTeamByID(id),
				Posts = postService.GetPostsByTeamID(id)
			};

			return View(postUserViewModel);
		}

		// GET: Posts/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create(int teamid)
        {
			if (teamid == 0)
			{
				return NotFound();
			}

			var team = postService.GetTeamByID(teamid);
			if (team == null)
			{
				return NotFound();
			}
			var postViewModel = new PostViewModel
			{
				Team = team
			};
			return View(postViewModel);
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = RoleConstraints.Administrator + "," + RoleConstraints.Moderator)]
		public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
				postViewModel.Author = await manager.GetUserAsync(HttpContext.User);

				Post post = AutoMapper.Mapper.Map<Post>(postViewModel);
				postService.AddPost(post);

                return RedirectToAction("Index", new RouteValueDictionary(
										new { id = postViewModel.TeamID }));
			}
			ViewData["Team"] = new SelectList(_context.Set<Team>(), "Name", "Name");

			return View(postViewModel);
        }

		// GET: Posts/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Post post = postService.FindPostByID(id);

			if (post == null)
			{
				return NotFound();
			}
			ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", post.AuthorID);

			PostViewModel postViewModel = AutoMapper.Mapper.Map<PostViewModel>(post);

			return View(postViewModel);
		}

		// POST: Posts/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleConstraints.Administrator + "," + RoleConstraints.Moderator)]
		public IActionResult Edit(int id, PostViewModel postViewModel)
		{
			if (id != postViewModel.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					Post post = AutoMapper.Mapper.Map<Post>(postViewModel);

					postService.UpdatePost(post);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!postService.PostExists(postViewModel.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Index", new RouteValueDictionary(
										new { id = postViewModel.TeamID }));
			}
			ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", postViewModel.AuthorID);

			return View(postViewModel);
		}

		// GET: Posts/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author).Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

		// POST: Posts/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleConstraints.Administrator + "," + RoleConstraints.Moderator)]
		public IActionResult DeleteConfirmed(int id)
		{
			var post = postService.FindPostByID(id);
			postService.DeletePost(post);

			return NoContent();
			//return RedirectToAction("Index", new RouteValueDictionary(
			//							new { id = post.TeamID })); ;
		}
	}
}
