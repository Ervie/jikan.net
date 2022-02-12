using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration for club types (search config).
	/// </summary>
	public enum ClubType
	{
		/// <summary>
		/// Allow all types to be displayed in results.
		/// </summary>
		[Description("")]
		EveryType,

		/// <summary>
		/// Public clubs
		/// </summary>
		[Description("public")]
		Public,

		/// <summary>
		/// Private clubs.
		/// </summary>
		[Description("private")]
		Private,

		/// <summary>
		/// Secret clubs.
		/// </summary>
		[Description("secret")]
		Secret
	}
}