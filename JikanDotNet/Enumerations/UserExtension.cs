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
	}
}