using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaNewsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaNewsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaNewsAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaNewsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaNewsAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaNewsAsync(1, page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaNewsAsync_OnePieceId_ShouldParseOnePieceNews()
	{
		// When
		var onePiece = await _tenrai.GetMangaNewsAsync(13);

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
		var onePiece = await _tenrai.GetMangaNewsAsync(13, 1);

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
		var onePiece = await _tenrai.GetMangaNewsAsync(13, 2);

		// Then
		using var _ = new AssertionScope();
		onePiece.Data.Should().NotBeEmpty();
		onePiece.Pagination.HasNextPage.Should().BeTrue();
		onePiece.Data.Select(x => x.Author).Should().Contain("Snow");
	}

	[Fact]
	public async Task GetMangaNewsAsync_MonsterId_ShouldParseMonsterNews()
	{
		// When
		var monster = await _tenrai.GetMangaNewsAsync(1);

		// Then
		using (new AssertionScope())
		{
			monster.Data.Should().HaveCount(11);
			monster.Data.Select(x => x.Author).Should().Contain("Xinil");
		}
	}
}