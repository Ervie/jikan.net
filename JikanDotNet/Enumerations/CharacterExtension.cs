using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration with possible extensions for character request.
	/// </summary>
	public enum CharacterExtension
	{
		/// <summary>
		/// Basic extension, no extra data.
		/// </summary>
		[Description("")]
		None,

		/// <summary>
		/// Pictures extension, return extra images of character.
		/// </summary>
		[Description("pictures")]
		Pictures
	}
}