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
		/// Manga.
		/// </summary>
		[Description("manga")]
		Manga,

		/// <summary>
		/// Light novel
		/// </summary>
		[Description("lightnovel")]
		Novel,

		/// <summary>
		/// Oneshot
		/// </summary>
		[Description("oneshot")]
		OneShot,

		/// <summary>
		/// Doijinshi
		/// </summary>
		[Description("doujin")]
		Doujinshi,

		/// <summary>
		/// Manhwa
		/// </summary>
		[Description("manhwa")]
		Manhwa,

		/// <summary>
		/// Manhua
		/// </summary>
		[Description("manhua")]
		Manhua
	}
}