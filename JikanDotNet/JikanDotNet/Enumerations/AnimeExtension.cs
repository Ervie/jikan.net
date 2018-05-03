using System.ComponentModel;

namespace JikanDotNet.Enumerations
{
	public enum AnimeExtension
	{
		[Description("")]
		None,

		[Description("episodes")]
		Episodes,

		[Description("characters_staff")]
		CharactersStaff,

		[Description("pictures")]
		Pictures,

		[Description("videos")]
		Videos,

		[Description("stats")]
		Stats,

		[Description("news")]
		News,

		[Description("forum")]
		Forum,

		[Description("more_info")]
		MoreInfo
	}
}