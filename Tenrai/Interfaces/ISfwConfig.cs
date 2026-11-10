namespace Tenrai.Interfaces;

/// <summary>
/// A config for an endpoint that takes <c>sfw</c>; null values fall back to the client's setting.
/// </summary>
internal interface ISfwConfig
{
	bool? Sfw { get; }

	bool? SfwStrict { get; }
}
