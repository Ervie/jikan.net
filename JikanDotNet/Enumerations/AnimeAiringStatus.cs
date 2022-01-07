using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Current status of anime.
	/// </summary>
	public enum AnimeAiringStatus
	{
		/// <summary>
		/// Finished Airing
		/// </summary>
		[Description("Finished Airing")]
		[EnumMember(Value = "Finished Airing")]
		FinishedAiring,

		/// <summary>
		/// Currently Airing.
		/// </summary>
		[Description("Currently Airing")]
		[EnumMember(Value = "Currently Airing")]
		CurrentlyAiring,

		/// <summary>
		/// Not yet aired.
		/// </summary>
		[Description("Not yet aired")]
		[EnumMember(Value = "Not yet aired")]
		NotYetAired
	}
}