using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// What an author is credited with on a manga.
/// </summary>
public enum AuthorRole
{
	/// <summary>
	/// The writing.
	/// </summary>
	[Description("story")]
	Story,

	/// <summary>
	/// The drawing.
	/// </summary>
	[Description("art")]
	Art
}
