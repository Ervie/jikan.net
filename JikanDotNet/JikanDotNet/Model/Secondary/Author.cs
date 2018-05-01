using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Author model class.
	/// </summary>
	public class Author
	{
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
	}
}