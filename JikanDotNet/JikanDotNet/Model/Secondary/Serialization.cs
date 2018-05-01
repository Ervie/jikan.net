using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Serialization model class.
	/// </summary>
	public class Serialization
	{
		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the serialization.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}