using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// A YouTube video from the top videos list, with the anime it was made for.
/// </summary>
public class TopVideo : AnimeTrailer
{
	/// <summary>
	/// The anime the video belongs to.
	/// </summary>
	[JsonPropertyName("entry")]
	public ICollection<TopVideoEntry> Entry { get; set; }
}
