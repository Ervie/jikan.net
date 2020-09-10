using System.Text.Json.Serialization;

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
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the licensor.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

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