using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.SeasonTests;

[Collection("TenraiTests")]
public class GetUpcomingSeasonAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetUpcomingSeasonAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetUpcomingSeasonAsync_ShouldParseUpcomingSeason()
	{
		// When
		var upcomingSeason = await _tenrai.GetUpcomingSeasonAsync();

		// Then
		using var _ = new AssertionScope();
		upcomingSeason.Pagination.HasNextPage.Should().BeTrue();
		upcomingSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(10);
		upcomingSeason.Pagination.CurrentPage.Should().Be(1);
		upcomingSeason.Pagination.Items.Count.Should().Be(25);
		upcomingSeason.Pagination.Items.Total.Should().BeGreaterThan(300);
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetUpcomingSeasonAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetUpcomingSeasonAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetUpcomingSeasonAsync_WithTooBigPage_ShouldParseAndReturnNothing()
	{
		// When
		var upcomingSeason = await _tenrai.GetUpcomingSeasonAsync(100);

		// Then
		upcomingSeason.Data.Should().BeEmpty();
	}

	[Fact]
	public async Task GetUpcomingSeasonAsync_PartialAiredDates_ShouldDeserialize()
	{
		// When
		var upcomingSeason = await _tenrai.GetUpcomingSeasonAsync();

		// Then
		upcomingSeason.Data.Should().Contain(a => a.Aired.From.HasValue && a.Aired.Parts.From.Day == null);
	}
}