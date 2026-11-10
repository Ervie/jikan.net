using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Anime or manga in a <see cref="RelatedEntry"/> group.
/// </summary>
public class RelationEntry : MalUrl
{
	/// <summary>
	/// Format of the related entry, e.g. "Manga", "Movie" or "TV Special".
	/// </summary>
	[JsonPropertyName("media_type")]
	public string MediaType { get; set; }

	/// <summary>
	/// Cover images of the related entry.
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSet Images { get; set; }
}
