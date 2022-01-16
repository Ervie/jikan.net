using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for person request.
	/// </summary>
	public enum PersonExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Pictures extension, return extra images of person.
		/// </summary>
		[Description("pictures")]
		Pictures,

		/// <summary>
		/// Anime extension, return animeography of person.
		/// </summary>
		[Description("anime")]
		Anime,

		/// <summary>
		/// Manga extension, return mangaography of person.
		/// </summary>
		[Description("manga")]
		Manga,

		/// <summary>
		/// Voices extension, return list of voice roles of a person.
		/// </summary>
		[Description("voices")]
		Voices
	}
}