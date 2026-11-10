using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Source material an anime is based on.
/// </summary>
public enum AnimeSource
{
	/// <summary>
	/// Original work.
	/// </summary>
	[Description("original")]
	Original,

	/// <summary>
	/// Manga.
	/// </summary>
	[Description("manga")]
	Manga,

	/// <summary>
	/// 4-koma manga.
	/// </summary>
	[Description("4_koma_manga")]
	FourKomaManga,

	/// <summary>
	/// Web manga.
	/// </summary>
	[Description("web_manga")]
	WebManga,

	/// <summary>
	/// Digital manga.
	/// </summary>
	[Description("digital_manga")]
	DigitalManga,

	/// <summary>
	/// Novel.
	/// </summary>
	[Description("novel")]
	Novel,

	/// <summary>
	/// Web novel.
	/// </summary>
	[Description("web_novel")]
	WebNovel,

	/// <summary>
	/// Light novel.
	/// </summary>
	[Description("light_novel")]
	LightNovel,

	/// <summary>
	/// Visual novel.
	/// </summary>
	[Description("visual_novel")]
	VisualNovel,

	/// <summary>
	/// Game.
	/// </summary>
	[Description("game")]
	Game,

	/// <summary>
	/// Card game.
	/// </summary>
	[Description("card_game")]
	CardGame,

	/// <summary>
	/// Book.
	/// </summary>
	[Description("book")]
	Book,

	/// <summary>
	/// Picture book.
	/// </summary>
	[Description("picture_book")]
	PictureBook,

	/// <summary>
	/// Radio.
	/// </summary>
	[Description("radio")]
	Radio,

	/// <summary>
	/// Music.
	/// </summary>
	[Description("music")]
	Music,

	/// <summary>
	/// Mixed media.
	/// </summary>
	[Description("mixed_media")]
	MixedMedia,

	/// <summary>
	/// Other.
	/// </summary>
	[Description("other")]
	Other,

	/// <summary>
	/// Unknown.
	/// </summary>
	[Description("unknown")]
	Unknown
}
