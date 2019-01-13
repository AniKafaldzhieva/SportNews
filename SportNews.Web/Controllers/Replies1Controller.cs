using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportNews.Models;
using SportNews.Web.Data;

namespace SportNews.Web.Controllers
{
    public class Replies1Controller : Controller
    {
        private readonly SportNewsDbContext _context;

        public Replies1Controller(SportNewsDbContext context)
        {
            _context = context;
        }

        // GET: Replies1
        public async Task<IActionResult> Index()
        {
            var sportNewsDbContext = _context.Replies.Include(r => r.Author).Include(r => r.Post);
            return View(await sportNewsDbContext.ToListAsync());
        }

        // GET: Replies1/Details/5
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

        // GET: Replies1/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID");
            return View();
        }

        // POST: Replies1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Content,CreatedOn,PostID,AuthorID")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", reply.AuthorID);
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reply.PostID);
            return View(reply);
        }

        // GET: Replies1/Edit/5
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
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", reply.AuthorID);
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reply.PostID);
            return View(reply);
        }

        // POST: Replies1/Edit/5
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
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", reply.AuthorID);
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reply.PostID);
            return View(reply);
        }

        // GET: Replies1/Delete/5
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

        // POST: Replies1/Delete/5
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
