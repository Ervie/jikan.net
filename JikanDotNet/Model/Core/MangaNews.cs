using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga news list model class.
	/// </summary>
	public class MangaNews: BaseJikanRequest
	{
		/// <summary>
		/// News related to manga.
		/// </summary>
		[JsonPropertyName("articles")]
		public ICollection<News> News { get; set; }
	}
}