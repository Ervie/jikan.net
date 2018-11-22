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
		All,

		/// <summary>
		/// Watching extension, parses only anime with watching status.
		/// </summary>
		[Description("watching")]
		Watching,

		/// <summary>
		/// Completed extension, parses only anime with completed status.
		/// </summary>
		[Description("completed")]
		Completed,

		/// <summary>
		/// On hold extension, parses only anime with on hold status.
		/// </summary>
		[Description("onhold")]
		OnHold,

		/// <summary>
		/// Dropped extension, parses only anime with dropped status.
		/// </summary>
		[Description("dropped")]
		Dropped,

		/// <summary>
		/// Plan to watch extension, parses only anime with plan to watch status.
		/// </summary>
		[Description("plantowatch")]
		PlanToWatch
	}
}