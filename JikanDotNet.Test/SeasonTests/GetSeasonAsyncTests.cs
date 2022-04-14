using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.SeasonTests
{
	public class GetSeasonAsyncTests
	{
		private readonly IJikan _jikan;

		public GetSeasonAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(999)]
		[InlineData(10000)]
		[InlineData(int.MaxValue)]
		public async Task GetSeasonAsync_InvalidYear_ShouldThrowValidationException(int year)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetSeasonAsync(year, Season.Fall));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((Season)int.MaxValue)]
		[InlineData((Season)int.MinValue)]
		public async Task GetSeasonAsync_InvalidSeasonValidYear_ShouldThrowValidationException(Season Season)
		{
			// When
			var func = this._jikan.Awaiting(x => x.GetSeasonAsync(2021, Season));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(1000)]
		[InlineData(1900)]
		[InlineData(2100)]
		[InlineData(9999)]
		public async Task GetSeasonAsync_ValidYearNotExistingSeason_ShouldReturnSeasonWithNulls(int year)
		{
			// When
			var season = await _jikan.GetSeasonAsync(year, Season.Winter);

			// Then
			season.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task GetSeasonAsync_Winter2000_ShouldParseWinter2000()
		{
			// When
			var winter2000 = await _jikan.GetSeasonAsync(2000, Season.Winter);

			// Then
			var titles = winter2000.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Boogiepop wa Warawanai");
				titles.Should().Contain("Ojamajo Doremi Sharp");
			}
		}

		[Fact]
		public async Task GetSeasonAsync_Spring1970_ShouldParseSpring1970()
		{
			// When
			var spring1970 = await _jikan.GetSeasonAsync(1970, Season.Spring);

			// Then
			spring1970.Data.Should().HaveCount(7);
		}

		[Fact]
		public async Task GetSeasonAsync_Winter2017_ShouldParseYoujoSenki()
		{
			// When
			var winter2017 = await _jikan.GetSeasonAsync(2017, Season.Winter);

			// Then
			using (new AssertionScope())
			{
				winter2017.Pagination.Items.Count.Should().Be(25);
				winter2017.Pagination.Items.Total.Should().Be(54);
				
				var youjoSenki = winter2017.Data.FirstOrDefault(x => x.Title.Equals("Youjo Senki"));

				youjoSenki.Type.Should().Be("TV");
				youjoSenki.Status.Should().Be("Finished Airing");
				youjoSenki.Episodes.Should().Be(12);
				youjoSenki.Airing.Should().BeFalse();
				youjoSenki.Duration.Should().Be("24 min per ep");
				youjoSenki.Rating.Should().Be("R - 17+ (violence & profanity)");
				youjoSenki.Score.Should().BeLessOrEqualTo(8.00);
				youjoSenki.ScoredBy.Should().BeGreaterThan(390000);
				youjoSenki.Members.Should().BeGreaterThan(740000);
				youjoSenki.Favorites.Should().BeGreaterThan(9000);
				youjoSenki.Season.Should().Be(Season.Winter);
				youjoSenki.Year.Should().Be(2017);
			}
		}
	}
}