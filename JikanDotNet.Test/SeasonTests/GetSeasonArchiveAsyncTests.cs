using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.SeasonTests
{
	public class GetSeasonArchiveAsyncTests
	{
		private readonly IJikan _jikan;

		public GetSeasonArchiveAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetSeasonArchiveAsync_NoParameter_ShouldParseFirstQueryableYear()
		{
			// When
			var seasonArchives = await _jikan.GetSeasonArchiveAsync();

			// Then
			var oldestSeason = seasonArchives.Data.Last();
			using (new AssertionScope())
			{
				oldestSeason.Year.Should().Be(1917);
				oldestSeason.Season.Should().HaveCount(4);
			}
		}

		[Fact]
		public async Task GetSeasonArchiveAsync_NoParameter_ShouldParseLatestQueryableYear()
		{
			// When
			var seasonArchives = await _jikan.GetSeasonArchiveAsync();

			// Then
			using (new AssertionScope())
			{
				seasonArchives.Data.First().Year.Should().BeGreaterOrEqualTo(DateTime.UtcNow.Year);
				seasonArchives.Data.Last().Season.Should().HaveCountGreaterOrEqualTo(1);
				seasonArchives.Data.Last().Season.Should().HaveCountLessOrEqualTo(4);
			}
		}
	}
}