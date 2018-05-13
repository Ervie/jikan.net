using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration for anime types (search config).
	/// </summary>
	public enum AnimeType
	{
		/// <summary>
		/// Allow all types to be displayed in results.
		/// </summary>
		[Description("")]
		EveryType,

		/// <summary>
		/// TV series.
		/// </summary>
		[Description("tv")]
		TV,

		/// <summary>
		/// Original video animation.
		/// </summary>
		[Description("ova")]
		OVA,

		/// <summary>
		/// Feature-lenght movie.
		/// </summary>
		[Description("movie")]
		Movie,

		/// <summary>
		/// A special episode.
		/// </summary>
		[Description("special")]
		Special,

		/// <summary>
		/// Original net animation.
		/// </summary>
		[Description("ona")]
		ONA,

		/// <summary>
		/// Music video.
		/// </summary>
		[Description("music")]
		Music
	}
}