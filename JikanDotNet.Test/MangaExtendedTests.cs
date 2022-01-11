using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaExtendedTests
	{
		private readonly IJikan _jikan;

		public MangaExtendedTests()
		{
			_jikan = new Jikan();
		}


		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviews_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaReviews>> func = _jikan.Awaiting(x => x.GetMangaReviews(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaReviews_BerserkId_ShouldParseBerserkReviews()
		{
			// When
			var berserk = await _jikan.GetMangaReviews(2);

			// Then
			using (new AssertionScope())
			{
				berserk.Reviews.First().Reviewer.Username.Should().Be("TheCriticsClub");
				berserk.Reviews.First().MalId.Should().Be(4403);
				berserk.Reviews.First().Votes.Should().BeGreaterThan(1200);

				berserk.Reviews.First().Reviewer.Scores.Overall.Should().Be(10);
				berserk.Reviews.First().Reviewer.Scores.Story.Should().Be(9);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviews_SecondPageInvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaReviews>> func = _jikan.Awaiting(x => x.GetMangaReviews(id, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviews_VvalidIdIndalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<MangaReviews>> func = _jikan.Awaiting(x => x.GetMangaReviews(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaReviews_BerserkIdSecondPage_ShouldParseBerserkReviewsPaged()
		{
			// When
			var berserk = await _jikan.GetMangaReviews(2, 2);

			// Then
			using (new AssertionScope())
			{
				berserk.Reviews.First().Reviewer.Username.Should().Be("Sibi_Gowtham");
				berserk.Reviews.First().MalId.Should().Be(261738);
				berserk.Reviews.First().Reviewer.ChaptersRead.Should().Be(351);
				berserk.Reviews.First().Votes.Should().BeGreaterThan(15);

				berserk.Reviews.First().Reviewer.Scores.Overall.Should().Be(4);
				berserk.Reviews.First().Reviewer.Scores.Story.Should().Be(5);
			}
		}
	}
}