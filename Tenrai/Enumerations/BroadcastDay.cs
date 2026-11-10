using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Day of the week an anime airs in Japan (JST), for the anime search.
/// </summary>
public enum BroadcastDay
{
	/// <summary>
	/// Monday.
	/// </summary>
	[Description("monday")]
	Monday,

	/// <summary>
	/// Tuesday.
	/// </summary>
	[Description("tuesday")]
	Tuesday,

	/// <summary>
	/// Wednesday.
	/// </summary>
	[Description("wednesday")]
	Wednesday,

	/// <summary>
	/// Thursday.
	/// </summary>
	[Description("thursday")]
	Thursday,

	/// <summary>
	/// Friday.
	/// </summary>
	[Description("friday")]
	Friday,

	/// <summary>
	/// Saturday.
	/// </summary>
	[Description("saturday")]
	Saturday,

	/// <summary>
	/// Sunday.
	/// </summary>
	[Description("sunday")]
	Sunday,

	/// <summary>
	/// Anime MyAnimeList lists with an irregular or unknown broadcast day.
	/// </summary>
	[Description("other")]
	Other,

	/// <summary>
	/// Today in Japan.
	/// </summary>
	[Description("today")]
	Today,

	/// <summary>
	/// Tomorrow in Japan.
	/// </summary>
	[Description("tomorrow")]
	Tomorrow
}
