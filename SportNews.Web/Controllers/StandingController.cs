using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportNews.Models;
using SportNews.Services.Interfaces;
using SportNews.Web.Data;
using SportNews.Web.ViewModels;

namespace SportNews.Web.Controllers
{
    public class StandingController : Controller
    {
        private readonly SportNewsDbContext _context;
        private readonly IStandingService standingService;

        public StandingController(SportNewsDbContext context, IStandingService standingService)
        {
            _context = context;
			this.standingService = standingService;
        }

        // GET: Standing
        public async Task<IActionResult> Index(string league)
        {
			if (league != null)
			{
				//standingService.AddTeams(int.Parse(league));
				standingService.AddStandings(int.Parse(league));
			}
			var sportNewsDbContext = _context.Standings.Include(s => s.League).Include(s => s.Team).OrderByDescending(t => t.Points);
			var model = new StandingViewModel();
			model.Countries = _context.Countries;
			model.Leagues = _context.Leagues;
			model.Standing = _context.Standings.Include(s => s.Team).Where(s => s.LeagueKey == league).OrderByDescending(t => t.Points);
			if (league != null)
			{
				model.Title = _context.Leagues.FirstOrDefault(l => l.LeagueID == int.Parse(league)).Name;

			}
            return View(model);
        }

        // GET: Standing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standing = await _context.Standings
                .Include(s => s.League)
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (standing == null)
            {
                return NotFound();
            }

            return View(standing);
        }

        // GET: Standing/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID");
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID");
            return View();
        }

        // POST: Standing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LeagueId,LeagueKey,LeagueSeason,LeagueRound,Place,PlaceType,TeamID,TeamKey,GamesPlayed,Wins,Draw,Losts,GoalsFor,GoalsAgainst,GoalDifference,Points,StandingUpdated")] Standing standing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", standing.LeagueId);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", standing.TeamID);
            return View(standing);
        }

        // GET: Standing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standing = await _context.Standings.FindAsync(id);
            if (standing == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", standing.LeagueId);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", standing.TeamID);
            return View(standing);
        }

        // POST: Standing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LeagueId,LeagueKey,LeagueSeason,LeagueRound,Place,PlaceType,TeamID,TeamKey,GamesPlayed,Wins,Draw,Losts,GoalsFor,GoalsAgainst,GoalDifference,Points,StandingUpdated")] Standing standing)
        {
            if (id != standing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandingExists(standing.ID))
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
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", standing.LeagueId);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", standing.TeamID);
            return View(standing);
        }

        // GET: Standing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standing = await _context.Standings
                .Include(s => s.League)
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (standing == null)
            {
                return NotFound();
            }

            return View(standing);
        }

        // POST: Standing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var standing = await _context.Standings.FindAsync(id);
            _context.Standings.Remove(standing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandingExists(int id)
        {
            return _context.Standings.Any(e => e.ID == id);
        }
    }
}
