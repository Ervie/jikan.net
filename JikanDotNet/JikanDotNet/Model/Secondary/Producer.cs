using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Producer model class.
	/// </summary>
	public class Producer
	{
		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the producer.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}