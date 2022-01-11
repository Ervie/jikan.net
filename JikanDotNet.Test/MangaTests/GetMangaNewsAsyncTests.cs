using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaNewsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaNewsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaNewsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaNewsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaNewsAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaNewsAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaNewsAsync_OnePieceId_ShouldParseOnePieceNews()
		{
			// When
			var onePiece = await _jikan.GetMangaNewsAsync(13);

			// Then
			using (new AssertionScope())
			{
				onePiece.Data.Should().HaveCount(30);
				onePiece.Data.Select(x => x.Author).Should().Contain("Aiimee");
			}
		}

		[Fact]
		public async Task GetMangaNewsAsync_OnePieceIdWithPage_ShouldParseOnePieceNews()
		{
			// When
			var onePiece = await _jikan.GetMangaNewsAsync(13, 1);

			// Then
			using (new AssertionScope())
			{
				onePiece.Data.Should().HaveCount(30);
				onePiece.Pagination.HasNextPage.Should().BeTrue();
				onePiece.Data.Select(x => x.Author).Should().Contain("Aiimee");
			}
		}

		[Fact]
		public async Task GetMangaNewsAsync_OnePieceIdWithNextPage_ShouldParseOnePieceNews()
		{
			// When
			var onePiece = await _jikan.GetMangaNewsAsync(13, 2);

			// Then
			using var _ = new AssertionScope();
			onePiece.Data.Should().NotBeEmpty();
			onePiece.Pagination.HasNextPage.Should().BeFalse();
			onePiece.Data.Select(x => x.Author).Should().Contain("Snow");
		}

		[Fact]
		public async Task GetMangaNewsAsync_MonsterId_ShouldParseMonsterNews()
		{
			// When
			var monster = await _jikan.GetMangaNewsAsync(1);

			// Then
			using (new AssertionScope())
			{
				monster.Data.Should().HaveCount(11);
				monster.Data.Select(x => x.Author).Should().Contain("Xinil");
			}
		}
	}
}