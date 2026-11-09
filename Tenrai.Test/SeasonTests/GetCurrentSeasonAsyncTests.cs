using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.SeasonTests
{
	[Collection("TenraiTests")]
	public class GetCurrentSeasonAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetCurrentSeasonAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetCurrentSeasonAsync_ShouldParseCurrentSeason()
		{
			// When
			var currentSeason = await _tenrai.GetCurrentSeasonAsync();

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(2);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(30);
		}
		
		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCurrentSeasonAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetCurrentSeasonAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}
		
		[Fact]
		public async Task GetCurrentSeasonAsync_WithTooBigPage_ShouldParseAndReturnNothing()
		{
			// When
			var upcomingSeason = await _tenrai.GetCurrentSeasonAsync(100);

			// Then
			upcomingSeason.Data.Should().BeEmpty();
		}
		
		[Fact]
		public async Task GetCurrentSeasonAsync_WithCorrectPage_ShouldParseCurrentSeason()
		{
			// When
			var currentSeason = await _tenrai.GetCurrentSeasonAsync(1);

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(2);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(3);
		}

		[Fact]
		public async Task GetCurrentSeasonAsync_IncludeContinuing_HasMore()
		{
			var plain = await _tenrai.GetCurrentSeasonAsync(new SeasonSearchConfig());
			var withContinuing = await _tenrai.GetCurrentSeasonAsync(new SeasonSearchConfig { IncludeContinuing = true });

			withContinuing.Pagination.Items.Total.Should().BeGreaterThan(plain.Pagination.Items.Total);
		}
	}
}