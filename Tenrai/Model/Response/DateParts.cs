using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// A date split into day, month and year, each null when unknown.
/// </summary>
public class DateParts
{
	/// <summary>
	/// Day of the month.
	/// </summary>
	[JsonPropertyName("day")]
	public int? Day { get; set; }

	/// <summary>
	/// Month, 1 to 12.
	/// </summary>
	[JsonPropertyName("month")]
	public int? Month { get; set; }

	/// <summary>
	/// Year.
	/// </summary>
	[JsonPropertyName("year")]
	public int? Year { get; set; }
}
