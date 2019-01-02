namespace SportNews.Web.Infrastructure.Mapping
{
	using AutoMapper;
	using SportNews.Models;
	using SportNews.Web.ViewModels;

	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<User, PostUserViewModel>();
			CreateMap<Post, PostUserViewModel>();
		}
	}
}
