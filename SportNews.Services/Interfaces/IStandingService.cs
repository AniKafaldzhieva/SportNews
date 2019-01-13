using SportNews.Models.JsonModels;
using System.Collections.Generic;

namespace SportNews.Services.Interfaces
{
	public interface IStandingService
	{
		void AddCountries();
		void AddLeagues();
		void AddTeams(int leagueId);
		void AddPlayers(int leagueId);
		void AddStandings(int leagueId);
		List<JsonStanding> GetStanding(int leagueID);
	}
}
