using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Club member model class.
	/// </summary>
	public class ClubMember
	{
		/// <summary>
		/// Club member's Username.
		/// </summary>
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		/// <summary>
		/// Club member's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Club member's URL
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }
	}
}