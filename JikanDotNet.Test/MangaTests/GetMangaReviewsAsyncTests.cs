using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaReviewsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaReviewsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviewsAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaReviewsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaReviewsAsync_BerserkId_ShouldParseBerserkReviews()
		{
			// When
			var berserk = await _jikan.GetMangaReviewsAsync(2);

			// Then
			using (new AssertionScope())
			{
				berserk.Data.First().User.Username.Should().Be("TheCriticsClub");
				berserk.Data.First().MalId.Should().Be(4403);
				berserk.Data.First().Votes.Should().BeGreaterThan(1200);
				berserk.Data.First().ReviewScores.Overall.Should().Be(10);
				berserk.Data.First().ReviewScores.Story.Should().Be(9);
			}
		}
	}
}