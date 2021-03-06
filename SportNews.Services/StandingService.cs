﻿namespace SportNews.Services
{
	using AutoMapper;
	using Microsoft.EntityFrameworkCore;
	using Newtonsoft.Json;
	using SportNews.Models;
	using SportNews.Models.JsonModels;
	using SportNews.Services.Interfaces;
	using SportNews.Web.Data;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;

	public class StandingService : IStandingService
	{
		private const string apikey2 = "14fc664cf4fd746a519457ca77c1f88b33bada2fca2e0c86211b5364242fec31";
		private const string apikey = "14fc664cf4fd746a519457ca77c1f88b33bada2fca2e0c86211b5364242fec31";
		private readonly SportNewsDbContext _context;

		public StandingService(SportNewsDbContext context)
		{
			_context = context;
		}

		public void AddCountries()
		{
			//var url = $"https://apifootball.com/api/?action=get_countries&APIkey={apikey}";
			//var json = new WebClient().DownloadString(url);
			//var result = JsonConvert.DeserializeObject<List<JsonCountry>>(json);

			var url = $"https://allsportsapi.com/api/football/?met=Countries&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonCountry>(json);

			//TODO
			//List<Country> b = Mapper.Map<List<JsonCountry>, List<Country>>(a);
			foreach (var jsonCountry in result.result)
			{
				Country country = Mapper.Map<Country>(jsonCountry);

				_context.Countries.Add(country);
			}
			_context.SaveChanges();
		}

		public void AddLeagues()
		{
			var url = $"https://allsportsapi.com/api/football/?met=Leagues&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonLeagues>(json);
			
			foreach (var jsonLeague in result.result)
			{
				League league = Mapper.Map<League>(jsonLeague);

				_context.Leagues.Add(league);
			}
			_context.SaveChanges();
		}

		public List<JsonStanding> GetStanding(int leagueID)
		{
			var url = $"https://allsportsapi.com/api/football/?&met=Standings&leagueId={leagueID}&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<Root>(json);
			var standings = result.result.total.ToList();

			return standings;
		}
		
		public void AddTeams(int leagueID)
		{
			var url = $"https://allsportsapi.com/api/football/?&met=Teams&leagueId={leagueID}&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonTeam>(json);

			foreach (var item in result.result)
			{
				string someUrl = item.team_logo;
				byte[] imageBytes = null;

				using (var webClient = new WebClient())
				{
					if (someUrl != null)
					{
						imageBytes = webClient.DownloadData(someUrl);
					}
				}
				
				var tempTeam = new Team()
				{
					TeamKey = int.Parse(item.team_key),
					Name = item.team_name,
					Category = Models.Enums.Categories.Футбол,
					LeagueId = leagueID,
					Badge = imageBytes
				};

				_context.Teams.Add(tempTeam);
			}

			_context.SaveChanges();
		}

		public void AddPlayers(int leagueID)
		{
			var url = $"https://allsportsapi.com/api/football/?&met=Teams&leagueId={leagueID}&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<JsonTeam>(json);

			foreach (var item in result.result)
			{
				foreach (var player in item.players.Where(p => p.player_age != "?"))
				{
					var p = Mapper.Map<Player>(player);
					p.Team = _context.Teams.FirstOrDefault(t => t.Name == item.team_name);
					_context.Add(p);
				}
			}

			_context.SaveChanges();
		}

		public void AddStandings(int leagueId)
		{
			var url = $"https://allsportsapi.com/api/football/?&met=Standings&leagueId={leagueId}&APIkey={apikey}";
			var json = new WebClient().DownloadString(url);
			var result = JsonConvert.DeserializeObject<Root>(json);

			foreach (var jsonStanding in result.result.total)
			{
				Standing standing = Mapper.Map<Standing>(jsonStanding);
				Team team = _context.Teams.FirstOrDefault(t => t.TeamKey.ToString() == jsonStanding.team_key);
				if (team == null)
				{
					AddTeams(int.Parse(jsonStanding.league_key));
				}
				standing.Team = team;
				standing.League = _context.Leagues.FirstOrDefault(t => t.LeagueID.ToString() == jsonStanding.league_key);
				Standing standingDB = _context.Standings.FirstOrDefault(s => s.TeamKey == standing.TeamKey);

				if (standingDB == null)
				{
					_context.Standings.Add(standing);
				}
				else
				{
					if (!Equals(standing, standingDB))
					{
						standing.ID = standingDB.ID;
						standing.LeagueId = standingDB.LeagueId;
						standing.TeamID = standingDB.TeamID;

						_context.Entry(standingDB).CurrentValues.SetValues(standing);
					}
				}
			}
			_context.SaveChanges();
		}
		private bool Equals(Standing obj, Standing obj2)
		{
			if (obj.League == obj2.League
				&& obj.LeagueKey == obj2.LeagueKey
				&& obj.LeagueRound == obj2.LeagueRound
				&& obj.LeagueSeason == obj2.LeagueSeason
				&& obj.Losts == obj2.Losts
				&& obj.Place == obj2.Place
				&& obj.PlaceType == obj2.PlaceType	
				&& obj.Points == obj2.Points		
				&& obj.TeamKey == obj2.TeamKey	
				&& obj.Wins == obj2.Wins	
				&& obj.Team == obj2.Team)
				return true;

			return false;
		}
	}
}
