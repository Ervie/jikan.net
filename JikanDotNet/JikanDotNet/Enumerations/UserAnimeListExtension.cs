using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for User anime list request.
	/// </summary>
	public enum UserAnimeListExtension
	{
		/// <summary>
		/// Basic extension, parse data for all entries.
		/// </summary>
		[Description("all")]
		All = 0,

		/// <summary>
		/// Watching extension, parses only anime with watching status.
		/// </summary>
		[Description("watching")]
		Watching = 1,

		/// <summary>
		/// Completed extension, parses only anime with completed status.
		/// </summary>
		[Description("completed")]
		Completed = 2,

		/// <summary>
		/// On hold extension, parses only anime with on hold status.
		/// </summary>
		[Description("onhold")]
		OnHold = 3,

		/// <summary>
		/// Dropped extension, parses only anime with dropped status.
		/// </summary>
		[Description("dropped")]
		Dropped = 4,

		/// <summary>
		/// Plan to watch extension, parses only anime with plan to watch status.
		/// </summary>
		[Description("plantowatch")]
		PlanToWatch = 6
	}
}