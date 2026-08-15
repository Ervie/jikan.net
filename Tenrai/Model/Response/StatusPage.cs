using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Details of a Tenrai status page.
	/// </summary>
	public class StatusPage
	{
		/// <summary>
		/// Name of the status page.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Link to the human readable status page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}
