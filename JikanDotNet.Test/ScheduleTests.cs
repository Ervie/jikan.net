using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ScheduleTests
	{
		private readonly IJikan _jikan;

		public ScheduleTests()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetSchedule_AllSchedule_ShouldParseCurrentSchedule()
		{
			// When
			var currentSeason = await _jikan.GetSchedule();

			// Then
			currentSeason.Should().NotBeNull();
		}

		[Fact]
		public async Task GetSchedule_Monday_ShouldParseMondaySchedule()
		{
			// When
			var currentSeason = await _jikan.GetSchedule(ScheduledDay.Monday);

			// Then
			var mondayScheduleTitles = currentSeason.Monday.Select(x => x.Title);
			using (new AssertionScope())
			{
				mondayScheduleTitles.Should().Contain("Golden Kamuy 3rd Season");
				mondayScheduleTitles.Should().Contain("Kingdom 3rd Season");
			}
		}

		[Fact]
		public async Task GetSchedule_Friday_ShouldParseFridaySchedule()
		{
			// When
			var currentSeason = await _jikan.GetSchedule(ScheduledDay.Friday);

			// Then
			var fridayScheduleTitles = currentSeason.Friday.Select(x => x.Title);
			using (new AssertionScope())
			{
				fridayScheduleTitles.Should().Contain("Adachi to Shimamura");
				fridayScheduleTitles.Should().Contain("Crayon Shin-chan");
			}
		}

		[Fact]
		public async Task GetSchedule_UnknownSchedule_ShouldParseUnknownSchedule()
		{
			// When
			var currentSeason = await _jikan.GetSchedule(ScheduledDay.Unknown);

			// Then
			var unknownScheduleTitles = currentSeason.Unknown.Select(x => x.Title);
			using (new AssertionScope())
			{
				unknownScheduleTitles.Should().Contain("Yodel no Onna");
				unknownScheduleTitles.Should().Contain("Jinxiu Shenzhou Zhi Qi You Ji");
			}
		}

		[Theory]
		[InlineData((ScheduledDay)int.MaxValue)]
		[InlineData((ScheduledDay)int.MinValue)]
		public async Task GetSchedule_InvalidScheduledDay_ShouldThrowValidationException(ScheduledDay schedule)
		{
			// When
			Func<Task<Schedule>> func = this._jikan.Awaiting(x => x.GetSchedule(schedule));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
	}
}