using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Licensor model class.
	/// </summary>
	public class Licensor
	{
		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the licensor.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}