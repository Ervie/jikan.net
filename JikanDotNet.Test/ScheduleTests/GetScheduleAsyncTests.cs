using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ScheduleTests
{
	public class GetScheduleAsyncTests
	{
		private readonly IJikan _jikan;

		public GetScheduleAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetScheduleAsync_AllSchedule_ShouldParseCurrentSchedule()
		{
			// When
			var currentSeason = await _jikan.GetScheduleAsync();

			// Then
			using var _ = new AssertionScope();
			currentSeason.Data.Should().NotBeEmpty();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterOrEqualTo(3);
		}

		[Fact]
		public async Task GetScheduleAsync_AllScheduleWithPage_ShouldParseCurrentSchedule()
		{
			// When
			var currentSeason = await _jikan.GetScheduleAsync(5);

			// Then
			using var _ = new AssertionScope();
			currentSeason.Data.Select(x => x.Title).Should().Contain("Sasaki to Miyano");
			currentSeason.Data.Select(x => x.Title).Should().Contain("Slow Loop");
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetScheduleAsync_WithInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetScheduleAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetScheduleAsync_AllScheduleWithTooBigPage_ShouldParseAndReturnEmpty()
		{
			// When
			var currentSeason = await _jikan.GetScheduleAsync(100);

			// Then
			currentSeason.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task GetScheduleAsync_Monday_ShouldParseMondaySchedule()
		{
			// When
			var currentSeason = await _jikan.GetScheduleAsync(ScheduledDay.Monday);

			// Then
			var mondayScheduleTitles = currentSeason.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				currentSeason.Pagination.HasNextPage.Should().BeFalse();
				currentSeason.Pagination.LastVisiblePage.Should().Be(1);
				mondayScheduleTitles.Should().Contain("Shingeki no Kyojin: The Final Season Part 2");
				mondayScheduleTitles.Should().Contain("Tribe Nine");
			}
		}

		[Fact]
		public async Task GetScheduleAsync_Friday_ShouldParseFridaySchedule()
		{
			// When
			var currentSeason = await _jikan.GetScheduleAsync(ScheduledDay.Friday);

			// Then
			var fridayScheduleTitles = currentSeason.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				fridayScheduleTitles.Should().Contain("Platinum End");
				fridayScheduleTitles.Should().Contain("Crayon Shin-chan");
			}
		}

		[Theory]
		[InlineData((ScheduledDay)int.MaxValue)]
		[InlineData((ScheduledDay)int.MinValue)]
		public async Task GetScheduleAsync_InvalidScheduledDay_ShouldThrowValidationException(ScheduledDay schedule)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetScheduleAsync(schedule));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
	}
}