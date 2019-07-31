using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "moreinfo")]
		public string Info { get; set; }
	}
}