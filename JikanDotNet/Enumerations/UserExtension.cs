using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for user request.
	/// </summary>
	public enum UserExtension
	{
		/// <summary>
		/// Basic extension, profile data.
		/// </summary>
		[Description("profile")]
		Profile,

		/// <summary>
		/// History extension, return data about user's history.
		/// </summary>
		[Description("history")]
		History,

		/// <summary>
		/// History extension, return data about user's friends.
		/// </summary>
		[Description("friends")]
		Friends,

		/// <summary>
		/// History extension, return data about user's anime list.
		/// </summary>
		[Description("animelist")]
		AnimeList,

		/// <summary>
		/// History extension, return data about user's manga list.
		/// </summary>
		[Description("mangalist")]
		MangaList,

		/// <summary>
		/// Statistics extension, return data about user's statistics.
		/// </summary>
		[Description("statistics")]
		Statistics,

		/// <summary>
		/// Favorites extension, return data about user's favorites.
		/// </summary>
		[Description("favorites")]
		Favorites,

		/// <summary>
		/// About extension, return data about user's description on the profile.
		/// </summary>
		[Description("about")]
		About,

		/// <summary>
		/// Reviews extension, return data about user's reviews.
		/// </summary>
		[Description("reviews")]
		Reviews,

		/// <summary>
		/// Recommendations extension, return data about user's recommendations.
		/// </summary>
		[Description("recommendations")]
		Recommendations
	}
}