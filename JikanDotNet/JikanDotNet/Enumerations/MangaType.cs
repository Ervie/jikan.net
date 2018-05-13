using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration for anime types (search config).
	/// </summary>
	public enum MangaType
	{
		/// <summary>
		/// Allow all types to be displayed in results.
		/// </summary>
		[Description("")]
		EveryType,

		/// <summary>
		/// TV series.
		/// </summary>
		[Description("manga")]
		Manga,

		/// <summary>
		/// Original video animation.
		/// </summary>
		[Description("novel")]
		Novel,

		/// <summary>
		/// Feature-lenght movie.
		/// </summary>
		[Description("oneshot")]
		OneShot,

		/// <summary>
		/// A special episode.
		/// </summary>
		[Description("doujin")]
		Doujinshi,

		/// <summary>
		/// Original net animation.
		/// </summary>
		[Description("manhwa")]
		Manhwa,

		/// <summary>
		/// Music video.
		/// </summary>
		[Description("manhua")]
		Manhua
	}
}