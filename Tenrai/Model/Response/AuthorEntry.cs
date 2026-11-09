using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Manga author with the part they had in the work.
	/// </summary>
	public class AuthorEntry : MalUrl
	{
		/// <summary>
		/// Part the author had, e.g. "Story &amp; Art", "Story" or "Art".
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}
