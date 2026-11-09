using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Anime, manga, people and characters an article is about.
	/// </summary>
	public class ArticleRelations : NewsRelations
	{
		/// <summary>
		/// Related people.
		/// </summary>
		[JsonPropertyName("people")]
		public ICollection<MalUrl> People { get; set; }

		/// <summary>
		/// Related characters.
		/// </summary>
		[JsonPropertyName("characters")]
		public ICollection<MalUrl> Characters { get; set; }
	}
}
