using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Role a character has in an anime or manga.
/// </summary>
public enum CharacterRole
{
	/// <summary>
	/// Main character.
	/// </summary>
	[Description("main")]
	Main,

	/// <summary>
	/// Supporting character.
	/// </summary>
	[Description("supporting")]
	Supporting
}
