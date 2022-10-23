using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.SeasonTests
{
	public class GetUpcomingSeasonAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUpcomingSeasonAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetUpcomingSeasonAsync_ShouldParseUpcomingSeason()
		{
			// When
			var upcomingSeason = await _jikan.GetUpcomingSeasonAsync();

			// Then
			using var _ = new AssertionScope();
			upcomingSeason.Pagination.HasNextPage.Should().BeTrue();
			upcomingSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(10);
			upcomingSeason.Pagination.CurrentPage.Should().Be(1);
			upcomingSeason.Pagination.Items.Count.Should().Be(25);
			upcomingSeason.Pagination.Items.Total.Should().BeGreaterThan(300);
			upcomingSeason.Data.Select(x => x.Title).Should().Contain("Goblin Slayer 2nd Season");
		}
		
		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUpcomingSeasonAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUpcomingSeasonAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
		
		[Fact]
		public async Task GetUpcomingSeasonAsync_WithTooBigPage_ShouldParseAndReturnNothing()
		{
			// When
			var upcomingSeason = await _jikan.GetUpcomingSeasonAsync(100);

			// Then
			upcomingSeason.Data.Should().BeEmpty();
		}
	}
}