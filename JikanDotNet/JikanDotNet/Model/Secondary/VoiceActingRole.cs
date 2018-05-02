using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Voice acting role model class for person's class.
	/// </summary>
	public class VoiceActingRole
	{
		/// <summary>
		/// Anime associated with voice acting role.
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
		public MALImageSubItem Anime { get; set; }

		/// <summary>
		/// Character associated with voice acting role.
		/// </summary>
		[JsonProperty(PropertyName = "character")]
		public MALImageSubItem Character { get; set; }
	}
}