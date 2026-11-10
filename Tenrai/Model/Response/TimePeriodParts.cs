using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Start and end of a <see cref="TimePeriod"/>, split into the pieces the source knows.
/// </summary>
public class TimePeriodParts
{
	/// <summary>
	/// Start of the period.
	/// </summary>
	[JsonPropertyName("from")]
	public DateParts From { get; set; }

	/// <summary>
	/// End of the period.
	/// </summary>
	[JsonPropertyName("to")]
	public DateParts To { get; set; }
}
