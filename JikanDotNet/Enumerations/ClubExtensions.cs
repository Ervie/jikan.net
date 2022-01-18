using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for anime request.
	/// </summary>
	public enum ClubExtensions
	{
		/// <summary>
		/// Basic extension, club profile information.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Members extension, return list of club members.
		/// </summary>
		[Description("members")]
		Members,

		/// <summary>
		/// Staff extension, return list of club staff.
		/// </summary>
		[Description("staff")]
		Staff,

		/// <summary>
		/// Members extension, return collections of related entities..
		/// </summary>
		[Description("relations")]
		Relations
	}
}