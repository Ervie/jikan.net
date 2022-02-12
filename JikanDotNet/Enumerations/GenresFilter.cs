using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration for filtering genres
	/// </summary>
	public enum GenresFilter
	{
		/// <summary>
		/// Genres.
		/// </summary>
		[Description("genres")]
		Genres,

		/// <summary>
		/// Explicit genres.
		/// </summary>
		[Description("explicit_genres")]
		ExplicitGenres,

		/// <summary>
		/// Themes.
		/// </summary>
		[Description("themes")]
		Themes,

		/// <summary>
		/// Themes.
		/// </summary>
		[Description("demographics")]
		Demographics
	}
}