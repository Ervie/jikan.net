using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaRecommendationsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaRecommendationsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaRecommendationsAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaRecommendationsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaRecommendationsAsync_BerserkId_ShouldParseBerserkRecommendations()
		{
			// When
			var berserk = await _jikan.GetMangaRecommendationsAsync(2);

			// Then
			using (new AssertionScope())
			{
				//Claymore
				berserk.Data.First().Entry.MalId.Should().Be(583);
				berserk.Data.First().Votes.Should().BeGreaterThan(25);
				berserk.Data.Count.Should().BeGreaterThan(90);
			}
		}
	}
}