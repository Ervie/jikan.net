using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaPicturesAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaPicturesAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaPicturesAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaPicturesAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaPicturesAsync_MonsterId_ShouldParseMonsterImages()
	{
		// When
		var monster = await _tenrai.GetMangaPicturesAsync(1);

		// Then
		monster.Data.Should().HaveCountGreaterOrEqualTo(8);
	}
}