using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for User manga list request.
	/// </summary>
	public enum UserMangaListExtension
	{
		/// <summary>
		/// Basic extension, parse data for all entries.
		/// </summary>
		[Description("all")]
		All,

		/// <summary>
		/// Reading extension, parses only manga with reading status.
		/// </summary>
		[Description("reading")]
		Reading,

		/// <summary>
		/// Completed extension, parses only manga with completed status.
		/// </summary>
		[Description("completed")]
		Completed,

		/// <summary>
		/// On hold extension, parses only manga with on hold status.
		/// </summary>
		[Description("onhold")]
		OnHold,

		/// <summary>
		/// Dropped extension, parses only manga with dropped status.
		/// </summary>
		[Description("dropped")]
		Dropped,

		/// <summary>
		/// Plan to read extension, parses only manga with plan to read status.
		/// </summary>
		[Description("plantoread")]
		PlanToRead
	}
}