using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// An anime a top video belongs to. Carries the list fields of <see cref="Anime"/> (no synopsis, broadcast, studios or genres) plus how MyAnimeList labels the video.
/// </summary>
public class TopVideoEntry : Anime
{
	/// <summary>
	/// MyAnimeList's label for the video on this anime, e.g. "PV 1" or "ED 1 (Anime ver.)".
	/// </summary>
	[JsonPropertyName("video_title")]
	public string VideoTitle { get; set; }

	/// <summary>
	/// Song title and artist. Null on promos and on some music videos.
	/// </summary>
	[JsonPropertyName("meta")]
	public MusicVideoMetadata Meta { get; set; }
}
