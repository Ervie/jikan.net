using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Type of entry a review targets. Used to filter the top reviews endpoint.
/// </summary>
public enum ReviewTargetType
{
	/// <summary>
	/// Anime reviews.
	/// </summary>
	[Description("anime")]
	Anime,

	/// <summary>
	/// Manga reviews.
	/// </summary>
	[Description("manga")]
	Manga
}
