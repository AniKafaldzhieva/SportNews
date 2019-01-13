using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
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
    public class ReplyController : Controller
    {
        private readonly SportNewsDbContext _context;
		private readonly IMapper _mapper;

		public ReplyController(SportNewsDbContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        // GET: Reply
        public async Task<IActionResult> Index(int postid)
        {
			//var sportNewsDbContext = _context.Replies.Where(p => p.PostID == postid).ToList();
			//var a = _context.Posts.Where(p => p.ID == postid).FirstOrDefault().Replies;
			//var c = this.User.Identity.Name;

			//var b = _context.Users.Where(p => p.UserName == c).FirstOrDefault().Replies;

			//var model = new ReplyViewModel();
			//model.Post = _context.Posts.Where(p => p.ID == postid).FirstOrDefault();
			//model.Replies = _context.Posts.Where(p => p.ID == postid).FirstOrDefault().Replies.ToList();
			var reply = _context.Replies
				.Include(r => r.Author)
				.Include(r => r.Post).Where(p => p.PostID == postid).ToList();
			//Mapper.Map<IList<Reply>, IList<ReplyViewModel>>(reply);
			var replies = await _context.Posts.Where(p => p.ID == postid)
					   .ProjectTo<ReplyViewModel>(_mapper)
					   .ToListAsync();
			//var replies = _mapper.Map<List<Reply>, List<ReplyViewModel>>(reply);
			var a = new ReplyViewModel() { Post = _context.Posts.Where(p => p.ID == postid).FirstOrDefault(), Replies = reply};
		
			return View(a);
        }

        // GET: Reply/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Author)
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

		[Authorize]
        // GET: Reply/Create
        public IActionResult Create(int postid)
        {
            ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id");
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID");

			var post = _context.Posts.Where(p => p.ID == postid).FirstOrDefault();
			var author = this.User.Identity.Name;
			var au = _context.Users.FirstOrDefault(u=>u.UserName == author);
			var model = new Reply { PostID = postid, Post = post, AuthorID = au.Id, Author = au };
			var a = new ReplyViewModel() { Post = post };
			return RedirectToAction("Index", new RouteValueDictionary(
										new { controller = "Reply", action = "Index", postid = post.ID }));
		}

        // POST: Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create([Bind("ID,Content,CreatedOn,PostID,AuthorID")] Reply reply)
        {
            if (ModelState.IsValid)
            {
				//reply.ID = 0;
				reply.CreatedOn = DateTime.Now;
				//reply.Post = _context.Posts.Where(p=>p.ID == id).FirstOrDefault();
                _context.Add(reply);
                await _context.SaveChangesAsync();
				return RedirectToAction("Index", new RouteValueDictionary(
										new { controller = "Reply", action = "Index", postid = reply.PostID }));
			}
            ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", reply.AuthorID);
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reply.PostID);
            return View(reply);
        }

        // GET: Reply/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", reply.AuthorID);
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reply.PostID);
            return View(reply);
        }

        // POST: Reply/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Content,CreatedOn,PostID,AuthorID")] Reply reply)
        {
            if (id != reply.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.ID))
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
            ViewData["AuthorID"] = new SelectList(_context.Set<User>(), "Id", "Id", reply.AuthorID);
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reply.PostID);
            return View(reply);
        }

        // GET: Reply/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Author)
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // POST: Reply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.ID == id);
        }
    }
}
