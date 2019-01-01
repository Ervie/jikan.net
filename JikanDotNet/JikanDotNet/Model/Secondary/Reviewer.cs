using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for base reviewer.
	/// </summary>
	public class Reviewer
	{
		/// <summary>
		/// Username.
		/// </summary>
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		/// <summary>
		/// Reviewer's image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Reviewer's URL.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }
	}
}