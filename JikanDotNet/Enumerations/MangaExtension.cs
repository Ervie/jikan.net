using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for manga request.
	/// </summary>
	public enum MangaExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Characters extension, return list of basic information about characters apeearing in the manga.
		/// </summary>
		[Description("characters")]
		Characters,

		/// <summary>
		/// Pictures extension, return extra images of manga.
		/// </summary>
		[Description("pictures")]
		Pictures,

		/// <summary>
		/// Statistics extension, return exact information about status and scores of manga among users.
		/// </summary>
		[Description("statistics")]
		Statistics,

		/// <summary>
		/// News extension, return basic information about all news related to manga.
		/// </summary>
		[Description("news")]
		News,

		/// <summary>
		/// Forum extension, return basic information about all forum topics related to manga.
		/// </summary>
		[Description("forum")]
		Forum,

		/// <summary>
		/// More info extension, return extra information stored in "more info" tab of manga.
		/// </summary>
		[Description("moreinfo")]
		MoreInfo,

		/// <summary>
		/// Recommendations extension, return extra information stored in "recommendation" tab of manga.
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
		Relations
	}
}