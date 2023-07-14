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
				berserk.Data.First().Score.Should().BeGreaterThan(7);
				berserk.Data.First().Reactions.TotalReactions.Should().BeGreaterThan(1);
			}
		}

		[Fact]
		public async Task GetMangaReviewsAsync_BerserkIdWithoutPreliminary_ShouldReturnEmpty()
		{
			// When
			var berserk = await _jikan.GetMangaReviewsAsync(2, includePreliminary: false);

			// Then
			berserk.Data.Should().BeEmpty();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviewsAsync_SecondPageWithInvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaReviewsAsync(malId, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviewsAsync_CorrectIdWrongPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaReviewsAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaReviewsAsync_BerserkIdSecondPage_ShouldParseBerserkReviewsSecondPage()
		{
			// When
			var berserk = await _jikan.GetMangaReviewsAsync(2, 2);

			// Then
			using (new AssertionScope())
			{
				berserk.Data.First().User.Username.Should().Be("tuant");
				berserk.Data.First().MalId.Should().Be(230026);
				berserk.Data.First().Score.Should().Be(4);
				berserk.Data.First().Reactions.TotalReactions.Should().BeGreaterThan(1);
			}
		}
	}
}