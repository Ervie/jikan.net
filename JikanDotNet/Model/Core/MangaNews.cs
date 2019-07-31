using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "articles")]
		public ICollection<News> News { get; set; }
	}
}