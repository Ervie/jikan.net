using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Age rating for anime (search config).
	/// </summary>
	public enum AgeRating
	{
		/// <summary>
		/// All ages.
		/// </summary>
		[Description("")]
		EveryRating,

		/// <summary>
		/// All ages.
		/// </summary>
		[Description("g")]
		G,

		/// <summary>
		/// Parental Guidance (Children).
		/// </summary>
		[Description("pg")]
		PG,

		/// <summary>
		/// Teens 13 or older.
		/// </summary>
		[Description("pg13")]
		PG13,

		/// <summary>
		/// Rated 17 and above (Violence and Profanity).
		/// </summary>
		[Description("r17")]
		R17,

		/// <summary>
		/// Mild nudity.
		/// </summary>
		[Description("r")]
		R,

		/// <summary>
		/// Adult (Hentai).
		/// </summary>
		[Description("rx")]
		RX
	}
}