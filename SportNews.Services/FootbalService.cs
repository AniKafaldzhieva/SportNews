namespace SportNews.Services
{
	using SportNews.Models;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;
	using System.Collections.Generic;
	using System.Linq;

	public class FootbalService : IFootballService
	{
		private readonly SportNewsDbContext context;

		public FootbalService(SportNewsDbContext context)
		{
			this.context = context;
		}

		public List<Country> GetAllCountries()
		{
			return context.Countries.ToList();
		}

		public List<League> GetAllLeagues()
		{
			return context.Leagues.ToList();
		}

		public League GetLeagueByID(string leagueID)
		{
			return context
				.Leagues
				.FirstOrDefault(l => l.LeagueID.ToString() == leagueID);
		}

		public Team GetTeamByKey(string teamKey)
		{
			return context
				.Teams
				.FirstOrDefault(t => t.TeamKey.ToString() == teamKey);
		}
	}
}
