using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Enumeration for manga types (search config).
/// </summary>
public enum MangaType
{
	/// <summary>
	/// Manga.
	/// </summary>
	[Description("manga")]
	Manga,

	/// <summary>
	/// Novel. Before 4.0 this member sent "lightnovel"; use <see cref="LightNovel"/> for those.
	/// </summary>
	[Description("novel")]
	Novel,

	/// <summary>
	/// Light novel.
	/// </summary>
	[Description("lightnovel")]
	LightNovel,

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