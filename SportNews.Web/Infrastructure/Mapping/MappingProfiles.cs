namespace SportNews.Web.Infrastructure.Mapping
{
	using AutoMapper;
	using SportNews.Models;
	using SportNews.Models.JsonModels;
	using SportNews.Web.ViewModels;
	using System.Collections.Generic;

	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<User, PostUserViewModel>();
			CreateMap<Post, PostUserViewModel>();
			CreateMap<Post, PostViewModel>();
			CreateMap<PostViewModel, Post>();
			CreateMap<TopicViewModel, Topic>();
			CreateMap<ReplyViewModel, Reply>();
			CreateMap<News, NewsDetailsViewModel>();
			CreateMap<NewsViewModel, News>();
			CreateMap<List<ReplyViewModel>, List<Reply>>();
			CreateMap<List<Topic>, List<TopicViewModel>>();
			CreateMap<JsonCountry.Country, Country>()
				.ForMember(d => d.CountryID, o => o.MapFrom(s => s.country_key))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.country_name));
			CreateMap<JsonLeagues.League, League>()
				.ForMember(d => d.LeagueID, o => o.MapFrom(s => s.league_key))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.league_name))
				.ForMember(d => d.CountryID, o => o.MapFrom(s => s.country_key));
			CreateMap<JsonTeam.TeamDTO, Team>()
				.ForMember(d => d.Name, o => o.MapFrom(s => s.team_name));
			CreateMap<JsonTeam.Player, Player>()
				.ForMember(d => d.PlayerKey, o => o.MapFrom(s => s.player_key))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.player_name))
				.ForMember(d => d.Country, o => o.MapFrom(s => s.player_country))
				.ForMember(d => d.Number, o => o.MapFrom(s => s.player_number))
				.ForMember(d => d.Type, o => o.MapFrom(s => s.player_type))
				.ForMember(d => d.Age, o => o.MapFrom(s => s.player_age))
				.ForMember(d => d.MatchPlayed, o => o.MapFrom(s => s.player_match_played))
				.ForMember(d => d.Goals, o => o.MapFrom(s => s.player_goals))
				.ForMember(d => d.YellowCards, o => o.MapFrom(s => s.player_yellow_cards))
				.ForMember(d => d.RedCards, o => o.MapFrom(s => s.player_red_cards));
			CreateMap<JsonStanding, Standing>()
				.ForMember(d => d.LeagueKey, o => o.MapFrom(s => s.league_key))
				.ForMember(d => d.LeagueRound, o => o.MapFrom(s => s.league_round))
				.ForMember(d => d.LeagueSeason, o => o.MapFrom(s => s.league_season))
				.ForMember(d => d.TeamKey, o => o.MapFrom(s => s.team_key))
				.ForMember(d => d.Place, o => o.MapFrom(s => s.standing_place))
				.ForMember(d => d.PlaceType, o => o.MapFrom(s => s.standing_place_type))
				.ForMember(d => d.Wins, o => o.MapFrom(s => s.standing_W))
				.ForMember(d => d.Draw, o => o.MapFrom(s => s.standing_D))
				.ForMember(d => d.Losts, o => o.MapFrom(s => s.standing_L))
				.ForMember(d => d.GamesPlayed, o => o.MapFrom(s => s.standing_P))
				.ForMember(d => d.GoalDifference, o => o.MapFrom(s => s.standing_GD))
				.ForMember(d => d.GoalsAgainst, o => o.MapFrom(s => s.standing_A))
				.ForMember(d => d.GoalsFor, o => o.MapFrom(s => s.standing_F))
				.ForMember(d => d.Points, o => o.MapFrom(s => s.standing_PTS))
				.ForMember(d => d.StandingUpdated, o => o.MapFrom(s => s.standing_updated))
				.ForMember(d => d.LeagueKey, o => o.MapFrom(s => s.league_key));
			CreateMap<JsonFixture.Result, Fixture>()
				.ForMember(d => d.EventKey, o => o.MapFrom(s => s.event_key))
				.ForMember(d => d.EventDate, o => o.MapFrom(s => s.event_date))
				.ForMember(d => d.EventTime, o => o.MapFrom(s => s.event_time))
				.ForMember(d => d.HalftTimeResult, o => o.MapFrom(s => s.event_halftime_result))
				.ForMember(d => d.FinalResult, o => o.MapFrom(s => s.event_final_result))
				.ForMember(d => d.HomeTeamKey, o => o.MapFrom(s => s.home_team_key))
				.ForMember(d => d.AwayTeamKey, o => o.MapFrom(s => s.away_team_key))
				.ForMember(d => d.CountryName, o => o.MapFrom(s => s.country_name))
				.ForMember(d => d.Status, o => o.MapFrom(s => s.event_status))
				.ForMember(d => d.LeagueKey, o => o.MapFrom(s => s.league_key))
				.ForMember(d => d.LeagueRound, o => o.MapFrom(s => s.league_round))
				.ForMember(d => d.LeagueSeason, o => o.MapFrom(s => s.league_season));
			//CreateMap<JsonFixture.Goalscorer, Player>()
			//	.ForMember(d => d.Name, o => o.MapFrom(s => s.home_scorer))
			//	.ForMember(d => d.Name, o => o.MapFrom(s => s.away_scorer));
			//CreateMap<JsonFixture.Goalscorer[], Player[]>();
			CreateMap<Fixture, FixtureViewModel>()
				.ForMember(d => d.EventKey, o => o.MapFrom(s => s.EventKey))
				.ForMember(d => d.EventDate, o => o.MapFrom(s => s.EventDate))
				.ForMember(d => d.EventTime, o => o.MapFrom(s => s.EventTime))
				.ForMember(d => d.EventStatus, o => o.MapFrom(s => s.Status))
				.ForMember(d => d.EventResult, o => o.MapFrom(s => s.FinalResult))
				.ForMember(d => d.HomeTeamKey, o => o.MapFrom(s => s.HomeTeamKey))
				.ForMember(d => d.AwayTeamKey, o => o.MapFrom(s => s.AwayTeamKey))
				.ForMember(d => d.LeagueKey, o => o.MapFrom(s => s.LeagueKey))
				.ForMember(d => d.LeagueRound, o => o.MapFrom(s => s.LeagueRound))
				.ForMember(d => d.LeagueSeason, o => o.MapFrom(s => s.LeagueSeason));
		}
	}
}
