using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.GetRandomTests;

[Collection("TenraiTests")]
public class GetRandomMangaAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetRandomMangaAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetRandomMangaAsync_ShouldReturnNotNullManga()
	{
		// When
		var manga = await _tenrai.GetRandomMangaAsync();

		// Then
		manga.Data.Should().NotBeNull();
	}
}