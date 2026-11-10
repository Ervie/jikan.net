using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Language of a voice cast.
/// </summary>
public enum VoiceLanguage
{
	/// <summary>
	/// English.
	/// </summary>
	[Description("english")]
	English,

	/// <summary>
	/// French.
	/// </summary>
	[Description("french")]
	French,

	/// <summary>
	/// German.
	/// </summary>
	[Description("german")]
	German,

	/// <summary>
	/// Hebrew.
	/// </summary>
	[Description("hebrew")]
	Hebrew,

	/// <summary>
	/// Hungarian.
	/// </summary>
	[Description("hungarian")]
	Hungarian,

	/// <summary>
	/// Italian.
	/// </summary>
	[Description("italian")]
	Italian,

	/// <summary>
	/// Japanese.
	/// </summary>
	[Description("japanese")]
	Japanese,

	/// <summary>
	/// Korean.
	/// </summary>
	[Description("korean")]
	Korean,

	/// <summary>
	/// Mandarin.
	/// </summary>
	[Description("mandarin")]
	Mandarin,

	/// <summary>
	/// Brazilian Portuguese.
	/// </summary>
	[Description("portuguese_br")]
	PortugueseBrazil,

	/// <summary>
	/// Spanish.
	/// </summary>
	[Description("spanish")]
	Spanish
}
