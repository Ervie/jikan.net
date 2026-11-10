using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Tenrai;

/// <summary>
/// Model class for base review.
/// </summary>
public class Review
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Review's URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; }

	/// <summary>
	/// Review's type.
	/// </summary>
	[JsonPropertyName("type")]
	public string Type { get; set; }

	/// <summary>
	/// Date of review creation.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTime? Date { get; set; }

	/// <summary>
	/// Review's content.
	/// </summary>
	[JsonPropertyName("review")]
	public string Content { get; set; }

	/// <summary>
	/// Reviewing user.
	/// </summary>
	[JsonPropertyName("user")]
	public UserMetadata User { get; set; }

	/// <summary>
	/// Review reactions.
	/// </summary>
	[JsonPropertyName("reactions")]
	public ReviewReactions Reactions { get; set; }

	/// <summary>
	/// Number of episodes watched by the reviewer (if review is about anime).
	/// </summary>
	[JsonPropertyName("episodes_watched")]
	public int? EpisodesWatched { get; set; }

	/// <summary>
	/// Number of chapters read by reviewer (if review is about manga).
	/// </summary>
	[JsonPropertyName("chapters_read")]
	public int? ChaptersRead { get; set; }

	/// <summary>
	/// The score of the review.
	/// </summary>
	[JsonPropertyName("score")]
	public int Score { get; set; }

	/// <summary>
	/// If the review contains spoilers.
	/// </summary>
	[JsonPropertyName("is_spoiler")]
	public bool IsSpoiler { get; set; }

	/// <summary>
	/// If the review is preliminary (written before the entry finished airing/publishing).
	/// </summary>
	[JsonPropertyName("is_preliminary")]
	public bool IsPreliminary { get; set; }

	/// <summary>
	/// Sentiment tags associated with the review (e.g. "Recommended", "Mixed Feelings", "Not Recommended").
	/// </summary>
	[JsonPropertyName("tags")]
	public ICollection<string> Tags { get; set; }

	/// <summary>
	/// Anime or manga the review is about. Set on recent and top reviews, null on an entry's own reviews.
	/// </summary>
	[JsonPropertyName("entry")]
	public MalImageSubItem Entry { get; set; }
}