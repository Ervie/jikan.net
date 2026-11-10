using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeCharactersAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeCharactersAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeCharactersAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeCharactersAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeCharactersStaffAsync_BebopId_ShouldParseCowboyBebopCharacters()
	{
		// When
		var bebop = await _tenrai.GetAnimeCharactersAsync(1);

		// Then
		bebop.Data.Should().Contain(x => x.Character.Name.Equals("Black, Jet"));
	}

	[Fact]
	public async Task GetAnimeCharactersAsync_BebopId_ShouldParseJetBlackPictures()
	{
		// When
		var bebop = await _tenrai.GetAnimeCharactersAsync(1);

		// Then
		var jetBlack = bebop.Data.First(x => x.Character.Name.Equals("Black, Jet"));
		using (new AssertionScope())
		{
			jetBlack.Character.Images.WebP.SmallImageUrl.Should().NotBeNullOrEmpty();
			jetBlack.Character.Images.WebP.ImageUrl.Should().NotBeNullOrEmpty();
			jetBlack.Character.Images.JPG.ImageUrl.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetAnimeCharactersAsync_BebopId_ShouldParseJetBlackFavorites()
	{
		// When
		var bebop = await _tenrai.GetAnimeCharactersAsync(1);

		// Then
		var jetBlack = bebop.Data.First(x => x.Character.Name.Equals("Black, Jet"));
		jetBlack.Favorites.Should().BeGreaterThan(1900);
	}

	[Fact]
	public async Task GetAnimeCharactersAsync_BebopId_ShouldParseSpikeSpiegelVoiceActors()
	{
		// When
		var bebop = await _tenrai.GetAnimeCharactersAsync(1);

		// Then
		var spikeSpiegel = bebop.Data.First(x => x.Character.MalId.Equals(1));
		using (new AssertionScope())
		{
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Language.Equals("Japanese"));
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Person.Name.Equals("Yamadera, Kouichi"));
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Language.Equals("English"));
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Person.Name.Equals("Blum, Steven"));
		}
	}
}