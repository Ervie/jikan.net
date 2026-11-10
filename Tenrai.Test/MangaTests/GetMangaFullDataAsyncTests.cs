using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaFullDataAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaFullDataAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaFullDataAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}


	[Fact]
	public async Task GetMangaFullDataAsync_MonsterId_ShouldParseMonster()
	{
		// When
		var monsterManga = await _tenrai.GetMangaFullDataAsync(1);

		// Then
		using var _ = new AssertionScope();
		monsterManga.Data.Title.Should().Be("Monster");
		monsterManga.Data.ExternalLinks.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://ja.wikipedia.org/wiki/MONSTER"));
		monsterManga.Data.Relations.Should().HaveCount(2);
		monsterManga.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 1);
		monsterManga.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 1);
	}

	[Fact]
	public async Task GetMangaFullDataAsync_BatchIds_ReturnsIncluded()
	{
		// When
		var berserk = await _tenrai.GetMangaFullDataAsync(2, new FullDataConfig { Ids = new List<long> { 1 } });

		// Then
		using var _ = new AssertionScope();
		berserk.Data.MalId.Should().Be(2);
		berserk.Included.Should().ContainSingle(x => x.MalId == 1);
		berserk.MissingIds.Should().BeEmpty();
	}
}