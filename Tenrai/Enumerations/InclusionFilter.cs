using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Tri-state filter: include entries of a kind, leave them out, or return only them.
/// </summary>
public enum InclusionFilter
{
	/// <summary>
	/// Include entries of this kind alongside the others (<c>true</c>).
	/// </summary>
	[Description("true")]
	Include,

	/// <summary>
	/// Exclude entries of this kind from the results (<c>false</c>).
	/// </summary>
	[Description("false")]
	Exclude,

	/// <summary>
	/// Return only entries of this kind (<c>only</c>).
	/// </summary>
	[Description("only")]
	Only
}
