namespace SportNews.Web.Controllers
{
	using System.Linq;
	using Microsoft.AspNetCore.Mvc;
	using SportNews.Services.Interfaces;
	using SportNews.Web.ViewModels;

	public class StandingController : Controller
    {
        private readonly IStandingService standingService;
        private readonly IFootballService footballService;

        public StandingController(IStandingService standingService, IFootballService footballService)
        {
			this.standingService = standingService;
			this.footballService = footballService;
        }

		// GET: Standing
		public IActionResult Index(string league)
		{
			if (league == null)
			{
				league = footballService.GetAllLeagues().FirstOrDefault().LeagueID.ToString();
			}

			standingService.AddStandings(int.Parse(league));

			StandingViewModel standingViewModel = new StandingViewModel();
			standingViewModel.Countries = footballService.GetAllCountries();
			standingViewModel.Leagues = footballService.GetAllLeagues();
			standingViewModel.Standing = footballService.GetStandingByKey(league);
			standingViewModel.Title = footballService.GetLeagueByID(league).Name;

			return View(standingViewModel);
		}

		// GET: Standing/Details/5
		//public async Task<IActionResult> Details(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return NotFound();
  //          }

  //          var standing = await _context.Standings
  //              .Include(s => s.League)
  //              .Include(s => s.Team)
  //              .FirstOrDefaultAsync(m => m.ID == id);
  //          if (standing == null)
  //          {
  //              return NotFound();
  //          }

  //          return View(standing);
  //      }

  //      // GET: Standing/Create
  //      public IActionResult Create()
  //      {
  //          ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID");
  //          ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID");
  //          return View();
  //      }

  //      // POST: Standing/Create
  //      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  //      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  //      [HttpPost]
  //      [ValidateAntiForgeryToken]
  //      public async Task<IActionResult> Create([Bind("ID,LeagueId,LeagueKey,LeagueSeason,LeagueRound,Place,PlaceType,TeamID,TeamKey,GamesPlayed,Wins,Draw,Losts,GoalsFor,GoalsAgainst,GoalDifference,Points,StandingUpdated")] Standing standing)
  //      {
  //          if (ModelState.IsValid)
  //          {
  //              _context.Add(standing);
  //              await _context.SaveChangesAsync();
  //              return RedirectToAction(nameof(Index));
  //          }
  //          ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", standing.LeagueId);
  //          ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", standing.TeamID);
  //          return View(standing);
  //      }

  //      // GET: Standing/Edit/5
  //      public async Task<IActionResult> Edit(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return NotFound();
  //          }

  //          var standing = await _context.Standings.FindAsync(id);
  //          if (standing == null)
  //          {
  //              return NotFound();
  //          }
  //          ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", standing.LeagueId);
  //          ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", standing.TeamID);
  //          return View(standing);
  //      }

  //      // POST: Standing/Edit/5
  //      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  //      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  //      [HttpPost]
  //      [ValidateAntiForgeryToken]
  //      public async Task<IActionResult> Edit(int id, [Bind("ID,LeagueId,LeagueKey,LeagueSeason,LeagueRound,Place,PlaceType,TeamID,TeamKey,GamesPlayed,Wins,Draw,Losts,GoalsFor,GoalsAgainst,GoalDifference,Points,StandingUpdated")] Standing standing)
  //      {
  //          if (id != standing.ID)
  //          {
  //              return NotFound();
  //          }

  //          if (ModelState.IsValid)
  //          {
  //              try
  //              {
  //                  _context.Update(standing);
  //                  await _context.SaveChangesAsync();
  //              }
  //              catch (DbUpdateConcurrencyException)
  //              {
  //                  if (!StandingExists(standing.ID))
  //                  {
  //                      return NotFound();
  //                  }
  //                  else
  //                  {
  //                      throw;
  //                  }
  //              }
  //              return RedirectToAction(nameof(Index));
  //          }
  //          ViewData["LeagueId"] = new SelectList(_context.Leagues, "ID", "ID", standing.LeagueId);
  //          ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "ID", standing.TeamID);
  //          return View(standing);
  //      }

  //      // GET: Standing/Delete/5
  //      public async Task<IActionResult> Delete(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return NotFound();
  //          }

  //          var standing = await _context.Standings
  //              .Include(s => s.League)
  //              .Include(s => s.Team)
  //              .FirstOrDefaultAsync(m => m.ID == id);
  //          if (standing == null)
  //          {
  //              return NotFound();
  //          }

  //          return View(standing);
  //      }

  //      // POST: Standing/Delete/5
  //      [HttpPost, ActionName("Delete")]
  //      [ValidateAntiForgeryToken]
  //      public async Task<IActionResult> DeleteConfirmed(int id)
  //      {
  //          var standing = await _context.Standings.FindAsync(id);
  //          _context.Standings.Remove(standing);
  //          await _context.SaveChangesAsync();
  //          return RedirectToAction(nameof(Index));
  //      }

  //      private bool StandingExists(int id)
  //      {
  //          return _context.Standings.Any(e => e.ID == id);
  //      }
    }
}
