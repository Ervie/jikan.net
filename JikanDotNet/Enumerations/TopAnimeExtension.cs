using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for top anime request.
	/// </summary>
	public enum TopAnimeExtension
	{
		/// <summary>
		/// Basic extension for top anime
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Top airing anime.
		/// </summary>
		[Description("airing")]
		TopAiring,

		/// <summary>
		/// Top upcoming anime.
		/// </summary>
		[Description("upcoming")]
		TopUpcoming,

		/// <summary>
		/// Top TV anime series.
		/// </summary>
		[Description("tv")]
		TopTV,

		/// <summary>
		/// Top movie anime.
		/// </summary>
		[Description("movie")]
		TopMovies,

		/// <summary>
		/// Top OVAs.
		/// </summary>
		[Description("ova")]
		TopOva,

		/// <summary>
		/// Top anime specials.
		/// </summary>
		[Description("special")]
		TopSpecial,

		/// <summary>
		/// Top anime by popularity.
		/// </summary>
		[Description("bypopularity")]
		TopPopularity,

		/// <summary>
		/// Top anime by favoritism.
		/// </summary>
		[Description("favorite")]
		TopFavorite
	}
}