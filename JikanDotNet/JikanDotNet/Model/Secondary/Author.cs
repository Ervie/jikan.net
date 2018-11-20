using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Author model class.
	/// </summary>
	public class Author
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the author.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Type.
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Overriden ToString method.
		/// </summary>
		/// <returns>Name if not null, base method elsewhere.</returns>
		public override string ToString()
		{
			return Name ?? base.ToString();
		}
	}
}