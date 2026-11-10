using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Producer with additional data model class.
/// </summary>
public class ProducerFull: Producer
{
	/// <summary>
	/// Producer's external links
	/// </summary>
	[JsonPropertyName("external")]
	public ICollection<ExternalLink> External { get; set; }
}