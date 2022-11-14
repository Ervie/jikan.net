using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.SeasonTests
{
	public class GetCurrentSeasonAsyncTests
	{
		private readonly IJikan _jikan;

		public GetCurrentSeasonAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetCurrentSeasonAsync_ShouldParseCurrentSeason()
		{
			// When
			var currentSeason = await _jikan.GetCurrentSeasonAsync();

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(2);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(30);
			currentSeason.Data.Select(x => x.Title).Should().Contain("Bleach: Sennen Kessen-hen");
		}
		
		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCurrentSeasonAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetCurrentSeasonAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
		
		[Fact]
		public async Task GetCurrentSeasonAsync_WithTooBigPage_ShouldParseAndReturnNothing()
		{
			// When
			var upcomingSeason = await _jikan.GetCurrentSeasonAsync(100);

			// Then
			upcomingSeason.Data.Should().BeEmpty();
		}
		
		[Fact]
		public async Task GetCurrentSeasonAsync_WithCorrectPage_ShouldParseCurrentSeason()
		{
			// When
			var currentSeason = await _jikan.GetCurrentSeasonAsync(1);

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(2);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(3);
			currentSeason.Data.Select(x => x.Title).Should().Contain("Bleach: Sennen Kessen-hen");
		}
	}
}