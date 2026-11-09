using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.AnimeTests
{
	[Collection("TenraiTests")]
	public class GetAnimeForumTopicsAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetAnimeForumTopicsAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeForumTopicsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetAnimeForumTopicsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetAnimeForumTopicsAsync_OnePiece_ReturnsFirstPage()
		{
			// When
			var onePiece = await _tenrai.GetAnimeForumTopicsAsync(21);

			// Then
			using var _ = new AssertionScope();
			onePiece.Data.Should().HaveCount(30);
			onePiece.Data.First().Title.Should().Be("One Piece Episode 1 Discussion");
			onePiece.Pagination.HasNextPage.Should().BeTrue();
		}

		[Fact]
		public async Task GetAnimeForumTopicsAsync_SecondPage_StartsAtEpisode31()
		{
			// When
			var onePiece = await _tenrai.GetAnimeForumTopicsAsync(21, new AnimeForumSearchConfig { Page = 2 });

			// Then
			onePiece.Data.First().Title.Should().Be("One Piece Episode 31 Discussion");
		}

		[Fact]
		public async Task GetAnimeForumTopicsAsync_ByCommentCountDesc_IsSorted()
		{
			// When
			var onePiece = await _tenrai.GetAnimeForumTopicsAsync(21, new AnimeForumSearchConfig
			{
				OrderBy = AnimeForumOrderBy.CommentCount,
				SortDirection = SortDirection.Descending
			});

			// Then
			onePiece.Data.Select(x => x.Comments).Should().BeInDescendingOrder();
		}
	}
}
