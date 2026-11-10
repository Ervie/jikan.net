using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Where entries without a value for the sort field go.
/// </summary>
public enum NullsOrder
{
	/// <summary>
	/// Before the entries with a value.
	/// </summary>
	[Description("first")]
	First,

	/// <summary>
	/// After the entries with a value.
	/// </summary>
	[Description("last")]
	Last,

	/// <summary>
	/// Left out of the results.
	/// </summary>
	[Description("exclude")]
	Exclude
}
