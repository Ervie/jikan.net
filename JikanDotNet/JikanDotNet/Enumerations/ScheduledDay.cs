using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Available options for schedule
	/// </summary>
	public enum ScheduledDay
	{
		/// <summary>
		/// Unknown schedule.
		/// </summary>
		[Description("unknown")]
		Unknown,

		/// <summary>
		/// Irregular airing.
		/// </summary>
		[Description("other")]
		Other,

		/// <summary>
		/// Monday.
		/// </summary>
		[Description("monday")]
		Monday,

		/// <summary>
		/// Tuesday.
		/// </summary>
		[Description("tuesday")]
		Tuesday,

		/// <summary>
		/// Wednesday.
		/// </summary>
		[Description("wednesday")]
		Wednesday,

		/// <summary>
		/// Thursday.
		/// </summary>
		[Description("thursday")]
		Thursday,

		/// <summary>
		/// Friday.
		/// </summary>
		[Description("friday")]
		Friday,

		/// <summary>
		/// Saturday.
		/// </summary>
		[Description("saturday")]
		Saturday,

		/// <summary>
		/// Sunday.
		/// </summary>
		[Description("sunday")]
		Sunday
	}
}