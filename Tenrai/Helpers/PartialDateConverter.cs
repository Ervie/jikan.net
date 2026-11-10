using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tenrai.Helpers;

internal sealed class PartialDateConverter : JsonConverter<DateTimeOffset?>
{
	// Unaired titles come back as "2027" or "2027-10"; those pad to the first day
	private static readonly string[] PartialFormats = { "yyyy", "yyyy-MM" };

	public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String) return null;

		var text = reader.GetString();
		const DateTimeStyles styles = DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal;
		if (DateTimeOffset.TryParseExact(text, PartialFormats, CultureInfo.InvariantCulture, styles, out var partial)) return partial;

		return DateTimeOffset.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var full) ? full : (DateTimeOffset?)null;
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
	{
		if (value.HasValue)
		{
			writer.WriteStringValue(value.Value);
		}
		else
		{
			writer.WriteNullValue();
		}
	}
}
