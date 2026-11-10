using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeMoreInfoAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeMoreInfoAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeMoreInfoAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeMoreInfoAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeMoreInfoAsync_BebopId_ShouldParseCowboyBebopMoreInfo()
	{
		// When
		var bebop = await _tenrai.GetAnimeMoreInfoAsync(1);

		// Then
		bebop.Data.Info.Should().StartWith("Suggested Order of Viewing");
	}
}