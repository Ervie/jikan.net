using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Current status of anime or (search config).
	/// </summary>
	public enum AiringStatus
	{
		/// <summary>
		/// Allow all statuses to be displayed in results.
		/// </summary>
		[Description("")]
		EveryStatus,
		
		/// <summary>
		/// Airing status.
		/// </summary>
		[Description("airing")]
		Airing,

		/// <summary>
		/// Complete status.
		/// </summary>
		[Description("complete")]
		Complete,

		/// <summary>
		/// Upcoming status.
		/// </summary>
		[Description("upcoming")]
		Upcoming
	}
}