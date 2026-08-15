using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Single entry of an interest stack.
	/// </summary>
	public class InterestStackEntry
	{
		/// <summary>
		/// Position of the entry within the stack, starting at 1.
		/// </summary>
		[JsonPropertyName("position")]
		public int Position { get; set; }

		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Entry's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Entry's images in various formats.
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

		/// <summary>
		/// Title of the entry.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the entry in English. Null if MyAnimeList has none.
		/// </summary>
		[JsonPropertyName("title_english")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Entry's type (e. g. "TV", "Movie", "Manga").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Score the stack's author gave this entry. Null if they did not score it.
		/// </summary>
		[JsonPropertyName("author_score")]
		public int? AuthorScore { get; set; }

		/// <summary>
		/// Note the stack's author attached to this entry. Null if they did not write one.
		/// </summary>
		[JsonPropertyName("note")]
		public string Note { get; set; }

		/// <summary>
		/// Entry's episode count. Only returned when the parent stack's <see cref="InterestStack.StackType">StackType</see> is "anime".
		/// </summary>
		[JsonPropertyName("episodes")]
		public int? Episodes { get; set; }

		/// <summary>
		/// Year the entry started airing. Only returned when the parent stack's <see cref="InterestStack.StackType">StackType</see> is "anime".
		/// </summary>
		[JsonPropertyName("aired_from_year")]
		public int? AiredFromYear { get; set; }

		/// <summary>
		/// Entry's volume count. Only returned when the parent stack's <see cref="InterestStack.StackType">StackType</see> is "manga".
		/// </summary>
		[JsonPropertyName("volumes")]
		public int? Volumes { get; set; }

		/// <summary>
		/// Year the entry started publishing. Only returned when the parent stack's <see cref="InterestStack.StackType">StackType</see> is "manga".
		/// </summary>
		[JsonPropertyName("published_from_year")]
		public int? PublishedFromYear { get; set; }
	}
}
