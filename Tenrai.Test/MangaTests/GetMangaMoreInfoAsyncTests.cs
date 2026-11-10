using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaMoreInfoAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaMoreInfoAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaMoreInfoAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaMoreInfoAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaMoreInfoAsync_BerserkId_ShouldParseBerserkMoreInfo()
	{
		// When
		var berserk = await _tenrai.GetMangaMoreInfoAsync(2);

		// Then
		berserk.Data.Info.Should().Contain("The Prototype (1988)");
	}
}