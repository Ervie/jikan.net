using System.ComponentModel;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration representing seasons of year.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum Season
	{
		/// <summary>
		/// Spring season.
		/// </summary>
		[Description("spring")]
		Spring,

		/// <summary>
		/// Summer season.
		/// </summary>
		[Description("summer")]
		Summer,

		/// <summary>
		/// Fall season.
		/// </summary>
		[Description("fall")]
		Fall,

		/// <summary>
		/// Winter season.
		/// </summary>
		[Description("winter")]
		Winter
	}
}