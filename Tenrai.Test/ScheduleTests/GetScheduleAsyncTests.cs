using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.ScheduleTests
{
	[Collection("TenraiTests")]
	public class GetScheduleAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetScheduleAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetScheduleAsync_AllSchedule_ShouldParseCurrentSchedule()
		{
			// When
			var currentSeason = await _tenrai.GetScheduleAsync();

			// Then
			using var _ = new AssertionScope();
			currentSeason.Data.Should().NotBeEmpty();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterOrEqualTo(3);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.PerPage.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(100);
		}

		[Fact]
		public async Task GetScheduleAsync_AllScheduleWithPage_ShouldParseCurrentSchedule()
		{
			// When
			var currentSeason = await _tenrai.GetScheduleAsync(3);

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.CurrentPage.Should().Be(3);
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.Items.Count.Should().Be(25);
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetScheduleAsync_WithInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetScheduleAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetScheduleAsync_AllScheduleWithTooBigPage_ShouldParseAndReturnEmpty()
		{
			// When
			var currentSeason = await _tenrai.GetScheduleAsync(100);

			// Then
			currentSeason.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task GetScheduleAsync_Monday_ShouldParseMondaySchedule()
		{
			// When
			var currentSeason = await _tenrai.GetScheduleAsync(ScheduledDay.Monday);

			// Then
			var mondayScheduleTitles = currentSeason.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				currentSeason.Pagination.HasNextPage.Should().BeFalse();
				currentSeason.Pagination.LastVisiblePage.Should().Be(1);
			}
		}

		[Fact]
		public async Task GetScheduleAsync_Friday_ShouldParseFridaySchedule()
		{
			// When
			var currentSeason = await _tenrai.GetScheduleAsync(ScheduledDay.Friday);

			// Then
			var fridayScheduleTitles = currentSeason.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				fridayScheduleTitles.Should().NotBeEmpty();
			}
		}

		[Theory]
		[InlineData((ScheduledDay)int.MaxValue)]
		[InlineData((ScheduledDay)int.MinValue)]
		public async Task GetScheduleAsync_InvalidScheduledDay_ShouldThrowValidationException(ScheduledDay schedule)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetScheduleAsync(schedule));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetScheduleAsync_MondayTv_OnlyTv()
		{
			var schedule = await _tenrai.GetScheduleAsync(new ScheduleSearchConfig { Day = ScheduledDay.Monday, Types = { AnimeType.TV } });

			using var _ = new AssertionScope();
			schedule.Data.Should().NotBeEmpty();
			schedule.Data.Should().OnlyContain(x => x.Type == "TV" && x.Broadcast.Day == "Mondays");
		}
	}
}