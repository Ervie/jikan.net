using System.Text.Json.Serialization;

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
		[JsonPropertyName("anime")]
		public MALImageSubItem Anime { get; set; }

		/// <summary>
		/// Character associated with voice acting role.
		/// </summary>
		[JsonPropertyName("character")]
		public MALImageSubItem Character { get; set; }

		/// <summary>
		/// Status of the role: Main/Supporting.
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}