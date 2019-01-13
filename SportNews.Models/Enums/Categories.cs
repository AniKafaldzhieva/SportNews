namespace SportNews.Models.Enums
{
	using System.ComponentModel.DataAnnotations;

	public enum Categories
	{
		[Display(Name = "Футбол")]
		Футбол,
		[Display(Name = "Баскетбол")]
		Баскетбол,
		[Display(Name = "Волейбол")]
		Волейбол,
		[Display(Name = "Тенис")]
		Тенис,
		[Display(Name = "Други")]
		Други
	}
}
