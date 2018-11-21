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
		/// Archive extension, return collection of available years and their seasons to query.
		/// </summary>
		[Description("archive")]
		Archive
	}
}