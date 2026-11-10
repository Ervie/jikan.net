using System;
using System.Text.Json.Serialization;
using Tenrai.Helpers;

namespace Tenrai;

/// <summary>
/// Anime trailer model class.
/// </summary>
public class AnimeTrailer
{
	/// <summary>
	/// ID associated with Youtube.
	/// </summary>
	[JsonPropertyName("youtube_id")]
	public string YoutubeId { get; set; }

	/// <summary>
	/// Url to the video.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; }

	/// <summary>
	/// Embed url to the video.
	/// </summary>
	[JsonPropertyName("embed_url")]
	public string EmbedUrl { get; set; }

	/// <summary>
	/// Image related to the trailer in various resolutions.
	/// </summary>
	[JsonPropertyName("images")]
	public Image Image { get; set; }

	/// <summary>
	/// Title of the video on YouTube.
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; }

	/// <summary>
	/// YouTube view count.
	/// </summary>
	[JsonPropertyName("views")]
	public long? Views { get; set; }

	/// <summary>
	/// YouTube like count.
	/// </summary>
	[JsonPropertyName("likes")]
	public long? Likes { get; set; }

	/// <summary>
	/// YouTube dislike count.
	/// </summary>
	[JsonPropertyName("dislikes")]
	public long? Dislikes { get; set; }

	/// <summary>
	/// YouTube comment count.
	/// </summary>
	[JsonPropertyName("comment_count")]
	public long? CommentCount { get; set; }

	/// <summary>
	/// When the video was published on YouTube.
	/// </summary>
	[JsonPropertyName("published_at")]
	public DateTimeOffset? PublishedAt { get; set; }

	/// <summary>
	/// Length of the video. The API sends ISO 8601, e.g. "PT56S".
	/// </summary>
	[JsonPropertyName("duration")]
	[JsonConverter(typeof(Iso8601DurationConverter))]
	public TimeSpan? Duration { get; set; }

	/// <summary>
	/// YouTube privacy status, e.g. "public" or "unlisted".
	/// </summary>
	[JsonPropertyName("privacy_status")]
	public string PrivacyStatus { get; set; }

	/// <summary>
	/// Countries where the video is allowed or blocked. Null when unrestricted.
	/// </summary>
	[JsonPropertyName("region_restriction")]
	public RegionRestriction RegionRestriction { get; set; }

	/// <summary>
	/// Whether the video can be embedded on other sites.
	/// </summary>
	[JsonPropertyName("embeddable")]
	public bool? Embeddable { get; set; }
}