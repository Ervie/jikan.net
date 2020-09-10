using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Extra information stored in "more info" tab.
	/// </summary>
	public class MoreInfo: BaseJikanRequest
	{
		/// <summary>
		/// Extra information stored in "more info" tab.
		/// </summary>
		[JsonPropertyName("moreinfo")]
		public string Info { get; set; }
	}
}