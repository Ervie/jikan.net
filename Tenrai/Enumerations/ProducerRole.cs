using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Job a company is credited with on an anime.
/// </summary>
public enum ProducerRole
{
	/// <summary>
	/// Animation studio.
	/// </summary>
	[Description("studio")]
	Studio,

	/// <summary>
	/// Producer.
	/// </summary>
	[Description("producer")]
	Producer,

	/// <summary>
	/// Licensor.
	/// </summary>
	[Description("licensor")]
	Licensor
}
