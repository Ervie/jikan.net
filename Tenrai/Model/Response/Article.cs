using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// MyAnimeList editorial article, as listed.
/// </summary>
public class Article
{
	/// <summary>
	/// Article MAL id.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Article URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; }

	/// <summary>
	/// Title.
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; }

	/// <summary>
	/// Short summary written by the author.
	/// </summary>
	[JsonPropertyName("summary")]
	public string Summary { get; set; }

	/// <summary>
	/// Publish time.
	/// </summary>
	[JsonPropertyName("published_at")]
	public DateTimeOffset? PublishedAt { get; set; }

	/// <summary>
	/// Author's MyAnimeList username.
	/// </summary>
	[JsonPropertyName("author_username")]
	public string Author { get; set; }

	/// <summary>
	/// Author's profile URL.
	/// </summary>
	[JsonPropertyName("author_url")]
	public string AuthorUrl { get; set; }

	/// <summary>
	/// View count.
	/// </summary>
	[JsonPropertyName("views")]
	public int Views { get; set; }

	/// <summary>
	/// Tags, e.g. "Spoiler" or "Analysis".
	/// </summary>
	[JsonPropertyName("tags")]
	public ICollection<string> Tags { get; set; }

	/// <summary>
	/// Header image.
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSet Images { get; set; }

	/// <summary>
	/// Opening text of the article.
	/// </summary>
	[JsonPropertyName("excerpt")]
	public string Excerpt { get; set; }
}
