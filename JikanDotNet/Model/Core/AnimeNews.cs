using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime news list model class.
	/// </summary>
	public class AnimeNews: BaseJikanRequest
	{
		/// <summary>
		/// News related to anime.
		/// </summary>
		[JsonPropertyName("articles")]
		public ICollection<News> News { get; set; }
	}
}