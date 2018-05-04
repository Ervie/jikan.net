using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for person request.
	/// </summary>
	public enum PersonExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Pictures extension, return extra images of person.
		/// </summary>
		[Description("pictures")]
		Pictures
	}
}