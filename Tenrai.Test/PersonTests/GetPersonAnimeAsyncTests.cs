using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.PersonTests;

[Collection("TenraiTests")]
public class GetPersonAnimeAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetPersonAnimeAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetPersonAnimeAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonAnimeAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetPersonAnimeAsync_YuasaId_ShouldParseMasaakiYuasaAnime()
	{
		// Given
		var yuasa = await _tenrai.GetPersonAnimeAsync(5068);

		// Then
		using (new AssertionScope())
		{
			yuasa.Data.Should().HaveCountGreaterThan(70);
			yuasa.Data.Should().Contain(x => x.Anime.Title.Equals("Ping Pong the Animation"));
			yuasa.Data.Should().Contain(x => x.Anime.Title.Equals("Yojouhan Shinwa Taikei"));
		}
	}

	[Fact]
	public async Task GetPersonAnimeAsync_SekiTomokazuId_ShouldParseSekiTomokazuAnime()
	{
		// Given
		var seki = await _tenrai.GetPersonAnimeAsync(1);

		// Then
		using (new AssertionScope())
		{
			seki.Data.Should().HaveCountLessThan(20);
			seki.Data.Should().Contain(x => x.Anime.Title.Equals("Anime Tenchou") && x.Position.Equals("add Theme Song Performance"));
		}
	}
}
