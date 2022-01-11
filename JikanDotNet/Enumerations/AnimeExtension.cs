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
		/// Characters extension, return list of basic information about characters appearing in the anime.
		/// </summary>
		[Description("characters")]
		Characters,

		/// <summary>
		/// Staff extension, return list of basic information about staff working on the anime.
		/// </summary>
		[Description("staff")]
		Staff,

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
		[Description("statistics")]
		Statistics,

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
		Recommendations,

		/// <summary>
		/// Reviews extension, return extra information stored in "reviews" tab of anime.
		/// </summary>
		[Description("reviews")]
		Reviews,

		/// <summary>
		/// User Updates extension, return extra information about user updates stored in "stats" tab of anime.
		/// </summary>
		[Description("userupdates")]
		UserUpdates,

		/// <summary>
		/// Relations extension, return related mangas/anime.
		/// </summary>
		[Description("relations")]
		Relations,

		/// <summary>
		/// Themes extension, return openings and endings.
		/// </summary>
		[Description("themes")]
		Themes
	}
}