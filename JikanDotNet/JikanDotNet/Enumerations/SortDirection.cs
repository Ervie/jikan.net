using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Defines sort direction in search requests
	/// </summary>
	public enum SortDirection
	{
		/// <summary>
		/// Use default sort direction.
		/// </summary>
		[Description("")]
		Default,

		/// <summary>
		/// Sort ascending.
		/// </summary>
		[Description("ascending")]
		Ascending,

		/// <summary>
		/// Use descending.
		/// </summary>
		[Description("descending")]
		Descending,
	}
}