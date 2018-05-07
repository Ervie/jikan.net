using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for anime request.
	/// </summary>
	public enum TopMangaExtension
	{
		/// <summary>
		/// Basic extension for top anime
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Top manga only.
		/// </summary>
		[Description("manga	")]
		TopManga,

		/// <summary>
		/// Top novels.
		/// </summary>
		[Description("novel")]
		TopNovel,

		/// <summary>
		/// Top one shots.
		/// </summary>
		[Description("oneshots")]
		TopOneShot,

		/// <summary>
		/// Top doujinshi.
		/// </summary>
		[Description("doujinshi")]
		TopDoujinshi,

		/// <summary>
		/// Top manhwa.
		/// </summary>
		[Description("manhwa")]
		TopManhwa,

		/// <summary>
		/// Top manhua.
		/// </summary>
		[Description("manhua")]
		TopManhua,

		/// <summary>
		/// Top manga by popularity.
		/// </summary>
		[Description("bypopularity")]
		TopPopularity,

		/// <summary>
		/// Top manga by favoritism.
		/// </summary>
		[Description("favorite")]
		TopFavorite
	}
}