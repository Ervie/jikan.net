using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for person request.
	/// </summary>
	public enum SeasonExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Upcoming extension, for anime with undefined airing date.
		/// </summary>
		[Description("upcoming")]
		Upcoming
	}
}