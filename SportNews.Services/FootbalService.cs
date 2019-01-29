namespace SportNews.Services
{
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class FootbalService : IFootballService
	{
		private readonly SportNewsDbContext context;

		public FootbalService(SportNewsDbContext context)
		{
			this.context = context;
		}

		public bool FindLeagueByID(string leagueID)
		{
			return context.Leagues.Any(l => l.LeagueID.ToString() == leagueID);
		}

		public List<Country> GetAllCountries()
		{
			return context.Countries.ToList();
		}

		public List<League> GetAllLeagues()
		{
			return context.Leagues.ToList();
		}

		public List<Team> GetAllTeams()
		{
			return context.Teams.ToList();
		}

		public List<Fixture> GetFixture(DateTime date, string id)
		{
			return context
				.Fixtures
				.Where(f => f.LeagueKey == id && f.EventDate == date.ToString("yyyy-MM-dd"))
				.ToList();
		}

		public League GetLeagueByID(string leagueID)
		{
			return context
				.Leagues
				.FirstOrDefault(l => l.LeagueID.ToString() == leagueID);
		}

		public List<Standing> GetStandingByKey(string leagueKey)
		{
			return context
				.Standings
				.Where(s => s.LeagueKey == leagueKey)
				.OrderBy(t => int.Parse(t.Place))
				.ToList();
		}

		public Team GetTeamByKey(string teamKey)
		{
			return context
				.Teams
				.FirstOrDefault(t => t.TeamKey.ToString() == teamKey);
		}
	}
}
