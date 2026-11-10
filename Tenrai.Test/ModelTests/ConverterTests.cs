using System;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.ModelTests;

public class ConverterTests
{
	[Theory]
	[InlineData("\"2006\"", 2006, 1, 1)]
	[InlineData("\"2027-10\"", 2027, 10, 1)]
	[InlineData("\"2023-09-29T00:00:00+00:00\"", 2023, 9, 29)]
	public void TimePeriod_From_ReadsFullAndPartialDates(string json, int year, int month, int day)
	{
		var period = JsonSerializer.Deserialize<TimePeriod>($"{{\"from\":{json}}}");

		period.From.Should().Be(new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero));
	}

	[Theory]
	[InlineData("null")]
	[InlineData("\"?\"")]
	public void TimePeriod_From_UnreadableOrNull_IsNull(string json)
	{
		var period = JsonSerializer.Deserialize<TimePeriod>($"{{\"from\":{json}}}");

		period.From.Should().BeNull();
	}

	[Theory]
	[InlineData("\"PT56S\"", 56)]
	[InlineData("\"PT1H2M3S\"", 3723)]
	public void AnimeTrailer_Duration_ReadsIso8601(string json, int seconds)
	{
		var trailer = JsonSerializer.Deserialize<AnimeTrailer>($"{{\"duration\":{json}}}");

		trailer.Duration.Should().Be(TimeSpan.FromSeconds(seconds));
	}

	[Theory]
	[InlineData("null")]
	[InlineData("\"abc\"")]
	public void AnimeTrailer_Duration_UnreadableOrNull_IsNull(string json)
	{
		var trailer = JsonSerializer.Deserialize<AnimeTrailer>($"{{\"duration\":{json}}}");

		trailer.Duration.Should().BeNull();
	}
}
