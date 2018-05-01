using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Studio model class.
	/// </summary>
	public class Genre
	{
		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the genre.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}