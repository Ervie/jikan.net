using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Studio model class.
	/// </summary>
	public class Studio
	{
		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the studio.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}