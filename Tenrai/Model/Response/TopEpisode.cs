using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// An episode from the top episodes list, with the anime it belongs to.
/// <see cref="AnimeEpisode.MalId"/> is the episode number, so two items can share it; <see cref="Entry"/> names the anime.
/// </summary>
public class TopEpisode : AnimeEpisode
{
	/// <summary>
	/// Number of users who voted on the episode.
	/// </summary>
	[JsonPropertyName("votes")]
	public int? Votes { get; set; }

	/// <summary>
	/// The anime the episode belongs to.
	/// </summary>
	[JsonPropertyName("entry")]
	public MalImageSubItem Entry { get; set; }
}
