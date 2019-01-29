namespace SportNews.Services.Interfaces
{
	using SportNews.Models;
	using System;
	using System.Collections.Generic;

	public interface IFootballService
	{
		bool FindLeagueByID(string leagueID);
		League GetLeagueByID(string leagueID);
		Team GetTeamByKey(string teamKey);
		List<Standing> GetStandingByKey(string leagueKey);
		List<League> GetAllLeagues();
		List<Country> GetAllCountries();
		List<Team> GetAllTeams();
		List<Fixture> GetFixture(DateTime date, string id);
	}
}
