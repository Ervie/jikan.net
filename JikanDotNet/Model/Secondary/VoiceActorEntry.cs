using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing voice actor entry on Character's page.
	/// </summary>
	public class VoiceActorEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Voice actor's name.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Url to voice actor main page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Voice actor's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Voice actor's language.
		/// </summary>
		[JsonProperty(PropertyName = "language")]
		public string Language { get; set; }

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