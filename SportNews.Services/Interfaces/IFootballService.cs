namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System.Collections.Generic;

	public interface IFootballService
	{
		League GetLeagueByID(string leagueID);
		Team GetTeamByKey(string teamKey);
		List<League> GetAllLeagues();
		List<Country> GetAllCountries();
	}
}
