using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.CatalogueTests
{
	[Collection("TenraiTests")]
	public class StatisticsHistoryTests
	{
		private readonly ITenrai _tenrai;

		public StatisticsHistoryTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetAnimeStatisticsHistoryAsync_Bebop_NewestFirst()
		{
			// When
			var history = await _tenrai.GetAnimeStatisticsHistoryAsync(1);

			// Then
			using var _ = new AssertionScope();
			history.Data.Should().NotBeEmpty();
			history.Data.Select(day => day.Date).Should().BeInDescendingOrder();
			history.Data.First().Members.Should().BeGreaterThan(2000000);
			history.Data.First().Statistics.Should().NotBeNull();
			history.Data.First().Statistics.Completed.Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task GetAnimeStatisticsHistoryAsync_BeforeApril2026_HasNoStatistics()
		{
			// Given
			var config = new StatisticsHistoryConfig
			{
				StartDate = new DateTime(2026, 4, 1),
				EndDate = new DateTime(2026, 4, 4),
				SortDirection = SortDirection.Ascending
			};

			// When
			var history = await _tenrai.GetAnimeStatisticsHistoryAsync(1, config);

			// Then
			using var _ = new AssertionScope();
			history.Data.Should().HaveCount(4);
			history.Data.Take(3).Should().OnlyContain(day => day.Statistics == null);
			history.Data.Last().Date.Should().Be(new DateTime(2026, 4, 4));
			history.Data.Last().Statistics.Should().NotBeNull();
		}

		[Fact]
		public async Task GetMangaStatisticsHistoryAsync_Berserk_HasReadingCounts()
		{
			// When
			var history = await _tenrai.GetMangaStatisticsHistoryAsync(2);

			// Then
			using var _ = new AssertionScope();
			history.Data.Should().NotBeEmpty();
			history.Data.First().Statistics.Reading.Should().BeGreaterThan(0);
			history.Data.First().Statistics.PlanToRead.Should().BeGreaterThan(0);
		}
	}
}
