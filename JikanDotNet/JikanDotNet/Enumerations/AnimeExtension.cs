using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for anime request.
	/// </summary>
	public enum AnimeExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Episodes extension, return data from "episodes" tab.
		/// </summary>
		[Description("episodes")]
		Episodes,

		/// <summary>
		/// Characters and staff extension, return list of basic information about characters appearing in the anime.
		/// </summary>
		[Description("characters_staff")]
		CharactersStaff,

		/// <summary>
		/// Pictures extension, return extra images of anime.
		/// </summary>
		[Description("pictures")]
		Pictures,

		/// <summary>
		/// Pictures extension, return information of videos related to anime.
		/// </summary>
		[Description("videos")]
		Videos,

		/// <summary>
		/// Statistics extension, return exact information about status and scores of anime among users.
		/// </summary>
		[Description("stats")]
		Stats,

		/// <summary>
		/// News extension, return basic information about all news related to anime.
		/// </summary>
		[Description("news")]
		News,

		/// <summary>
		/// Forum extension, return basic information about all forum topics related to anime.
		/// </summary>
		[Description("forum")]
		Forum,

		/// <summary>
		/// More info extension, return extra information stored in "more info" tab of anime.
		/// </summary>
		[Description("moreinfo")]
		MoreInfo,

		/// <summary>
		/// Recommendations extension, return extra information stored in "recommendation" tab of anime.
		/// </summary>
		[Description("recommendations")]
		Recommendations
	}
}