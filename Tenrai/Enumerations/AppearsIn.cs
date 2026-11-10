using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Kinds of entries a character appears in.
/// </summary>
public enum AppearsIn
{
	/// <summary>
	/// Only in anime.
	/// </summary>
	[Description("anime")]
	Anime,

	/// <summary>
	/// Only in manga.
	/// </summary>
	[Description("manga")]
	Manga,

	/// <summary>
	/// In both anime and manga.
	/// </summary>
	[Description("both")]
	Both
}
