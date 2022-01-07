using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration for anime types (search config).
	/// </summary>
	public enum AnimeType
	{
		/// <summary>
		/// TV series.
		/// </summary>
		[Description("TV")]
		TV,

		/// <summary>
		/// Original video animation.
		/// </summary>
		[Description("OVA")]
		OVA,

		/// <summary>
		/// Feature-lenght movie.
		/// </summary>
		[Description("Movie")]
		Movie,

		/// <summary>
		/// A special episode.
		/// </summary>
		[Description("Special")]
		Special,

		/// <summary>
		/// Original net animation.
		/// </summary>
		[Description("ONA")]
		ONA,

		/// <summary>
		/// Music video.
		/// </summary>
		[Description("Music")]
		Music,

		/// <summary>
		/// Allow all types to be displayed in results.
		/// </summary>
		[Description("")]
		EveryType
	}
}