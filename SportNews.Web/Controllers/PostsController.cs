using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SportNews.Models;
using SportNews.Web.Data;
using SportNews.Web.ViewModels;

namespace SportNews.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly SportNewsDbContext _context;
		private readonly UserManager<User> manager;

        public PostsController(SportNewsDbContext context, UserManager<User> manager)
        {
            _context = context;
			this.manager = manager;
        }

        // GET: Posts
        public async Task<IActionResult> Index(int id)
        {
            var sportNewsDbContext = _context.Posts.Where(p=>p.Team.ID == id);
			var model = new PostUserViewModel();
			model.TeamID = id;
			model.Team = _context.Teams.FirstOrDefault(t => t.ID == id);
			model.Posts= _context.Posts.Where(p=>p.Team.ID == id).ToList();
			return View(model);
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

			var team = _context.Teams
				.FirstOrDefault(m => m.ID == teamid);
			if (team == null)
			{
				return NotFound();
			}
			var post = new Post();
			post.Team = team;
			return View(post);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
				post.Author = await this.manager.GetUserAsync(HttpContext.User);
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new RouteValueDictionary(
										new { id = post.TeamID }));
			}
			ViewData["Team"] = new SelectList(_context.Set<Team>(), "Name", "Name");

			return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", post.AuthorID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Content,CreatedOn,AuthorID")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
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
            ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", post.AuthorID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }
    }
}
