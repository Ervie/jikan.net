using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for character request.
	/// </summary>
	public enum CharacterExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Pictures extension, return extra images of character.
		/// </summary>
		[Description("pictures")]
		Pictures,

		/// <summary>
		/// Anime extension, return animeography of character.
		/// </summary>
		[Description("anime")]
		Anime,

		/// <summary>
		/// Manga extension, return mangaography of character.
		/// </summary>
		[Description("manga")]
		Manga,

		/// <summary>
		/// Voices extension, return list of voice actors of a characters.
		/// </summary>
		[Description("voices")]
		Voices
	}
}