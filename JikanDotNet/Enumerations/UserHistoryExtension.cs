using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for User history request.
	/// </summary>
	public enum UserHistoryExtension
	{
		/// <summary>
		/// Basic extension, parse data for both anime and manga history.
		/// </summary>
		[Description("")]
		Both,

		/// <summary>
		/// Anime extension, parses only history about anime.
		/// </summary>
		[Description("anime")]
		Anime,

		/// <summary>
		/// Manga extension, parses only history about manga.
		/// </summary>
		[Description("manga")]
		Manga
	}
}