using System.ComponentModel;

namespace Tenrai
{
	/// <summary>
	/// Enumeration for interest stack types (search config).
	/// </summary>
	public enum InterestStackType
	{
		/// <summary>
		/// Stack of anime entries.
		/// </summary>
		[Description("anime")]
		Anime,

		/// <summary>
		/// Stack of manga entries.
		/// </summary>
		[Description("manga")]
		Manga,

		/// <summary>
		/// Allow all types to be displayed in results.
		/// </summary>
		[Description("")]
		EveryType
	}
}
