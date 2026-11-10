using FluentAssertions;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaCharactersAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaCharactersAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaCharactersAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaCharactersAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaCharactersAsync_MonsterId_ShouldParseMonsterCharacters()
	{
		// When
		var monster = await _tenrai.GetMangaCharactersAsync(1);

		// Then
		monster.Data.Should().HaveCountGreaterOrEqualTo(100);
	}

	[Fact]
	public async Task GetMangaCharactersAsync_MonsterId_ShouldParseMonsterCharactersJohan()
	{
		// When
		var monster = await _tenrai.GetMangaCharactersAsync(1);

		// Then
		monster.Data.Select(x => x.Character.Name).Should().Contain("Liebert, Johan");
	}
}