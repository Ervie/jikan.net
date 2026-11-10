using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Tenrai.Helpers;

// System.Text.Json reads TimeSpan only as "00:00:56"; the API sends ISO 8601 ("PT56S")
internal sealed class Iso8601DurationConverter : JsonConverter<TimeSpan?>
{
	public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String) return null;

		try
		{
			return XmlConvert.ToTimeSpan(reader.GetString());
		}
		catch (FormatException)
		{
			return null;
		}
	}

	public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
	{
		if (value.HasValue)
		{
			writer.WriteStringValue(XmlConvert.ToString(value.Value));
		}
		else
		{
			writer.WriteNullValue();
		}
	}
}
