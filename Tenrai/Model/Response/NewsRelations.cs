using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Anime and manga a news item is about.
/// </summary>
public class NewsRelations
{
	/// <summary>
	/// Related anime.
	/// </summary>
	[JsonPropertyName("anime")]
	public ICollection<MalUrl> Anime { get; set; }

	/// <summary>
	/// Related manga.
	/// </summary>
	[JsonPropertyName("manga")]
	public ICollection<MalUrl> Manga { get; set; }
}
