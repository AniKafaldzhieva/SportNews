using SportNews.Models;
using System.Collections.Generic;

namespace SportNews.Web.ViewModels
{
	public class StandingViewModel
	{
		public string Title { get; set; }
		public IEnumerable<Country> Countries { get; set; }
		public IEnumerable<League> Leagues { get; set; }
		public IEnumerable<Standing> Standing { get; set; }
	}
}
