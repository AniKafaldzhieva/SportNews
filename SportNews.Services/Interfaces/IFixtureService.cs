namespace SportNews.Services.Interfaces
{
	using SportNews.Models.JsonModels;
	using System;

	public interface IFixtureService
	{
		JsonFixture AddFixture(DateTime date, string leagueId);
		JsonFixture Livescore(string leagueId);
	}
}