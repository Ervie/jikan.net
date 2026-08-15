using System;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Interest stack model class. An interest stack is a user curated, ordered list of anime or manga.
	/// </summary>
	public class InterestStack
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Stack's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Type of entries the stack holds ("anime" or "manga").
		/// </summary>
		[JsonPropertyName("stack_type")]
		public string StackType { get; set; }

		/// <summary>
		/// Title of the stack.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Description written by the stack's author. Empty string if the author did not write one.
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; }

		/// <summary>
		/// MyAnimeList username of the stack's author.
		/// </summary>
		[JsonPropertyName("author_username")]
		public string AuthorUsername { get; set; }

		/// <summary>
		/// Link to the profile of the stack's author.
		/// </summary>
		[JsonPropertyName("author_url")]
		public string AuthorUrl { get; set; }

		/// <summary>
		/// Is the stack curated by MyAnimeList staff.
		/// </summary>
		[JsonPropertyName("is_official")]
		public bool IsOfficial { get; set; }

		/// <summary>
		/// Is the stack marked as a challenge.
		/// </summary>
		[JsonPropertyName("is_challenge")]
		public bool IsChallenge { get; set; }

		/// <summary>
		/// Is the stack marked as containing spoilers.
		/// </summary>
		[JsonPropertyName("is_spoiler")]
		public bool IsSpoiler { get; set; }

		/// <summary>
		/// Number of users who have restacked this stack.
		/// </summary>
		[JsonPropertyName("restack_count")]
		public int RestackCount { get; set; }

		/// <summary>
		/// Number of entries in the stack.
		/// </summary>
		[JsonPropertyName("entry_count")]
		public int EntryCount { get; set; }

		/// <summary>
		/// Date the stack was created.
		/// </summary>
		[JsonPropertyName("created_at")]
		public DateTime? CreatedAt { get; set; }
	}
}
