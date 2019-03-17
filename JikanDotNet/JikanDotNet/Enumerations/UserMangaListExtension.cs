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
		All = 0,

		/// <summary>
		/// Reading extension, parses only manga with reading status.
		/// </summary>
		[Description("reading")]
		Reading = 1,

		/// <summary>
		/// Completed extension, parses only manga with completed status.
		/// </summary>
		[Description("completed")]
		Completed = 2,

		/// <summary>
		/// On hold extension, parses only manga with on hold status.
		/// </summary>
		[Description("onhold")]
		OnHold = 3,

		/// <summary>
		/// Dropped extension, parses only manga with dropped status.
		/// </summary>
		[Description("dropped")]
		Dropped = 4,

		/// <summary>
		/// Plan to read extension, parses only manga with plan to read status.
		/// </summary>
		[Description("plantoread")]
		PlanToRead = 6
	}
}