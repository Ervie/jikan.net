using System.Text.Json.Serialization;
using System;
using Tenrai.Helpers;

namespace Tenrai;

/// <summary>
/// Model class for time periods, e. g. dates of start and end manga publishing.
/// </summary>
public class TimePeriod
{
	/// <summary>
	/// Start date. Uses <see cref="DateTimeOffset"/> to preserve the UTC offset returned by the API
	/// (ISO-8601 timestamps with explicit "+00:00" offset) and avoid local-time conversions.
	/// A partial date ("2027-10") is padded to its first day; <see cref="Parts"/> says which pieces are known.
	/// </summary>
	[JsonPropertyName("from")]
	[JsonConverter(typeof(PartialDateConverter))]
	public DateTimeOffset? From { get; set; }

	/// <summary>
	/// End date. Uses <see cref="DateTimeOffset"/> to preserve the UTC offset returned by the API
	/// (ISO-8601 timestamps with explicit "+00:00" offset) and avoid local-time conversions.
	/// A partial date ("2027-10") is padded to its first day; <see cref="Parts"/> says which pieces are known.
	/// </summary>
	[JsonPropertyName("to")]
	[JsonConverter(typeof(PartialDateConverter))]
	public DateTimeOffset? To { get; set; }

	/// <summary>
	/// Day, month and year of each end, null where the source does not know them.
	/// </summary>
	[JsonPropertyName("prop")]
	public TimePeriodParts Parts { get; set; }

	/// <summary>
	/// The period as display text, e.g. "Sep 29, 2023 to Mar 22, 2024" or "Oct 2027 to ?".
	/// </summary>
	[JsonPropertyName("string")]
	public string Text { get; set; }
}