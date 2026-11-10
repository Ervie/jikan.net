using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.PersonTests;

[Collection("TenraiTests")]
public class GetPersonVoiceActingRolesAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetPersonVoiceActingRolesAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetPersonVoiceActingRolesAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonVoiceActingRolesAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetPersonVoiceActingRolesAsync_YuasaId_ShouldParseMasaakiYuasaVoiceActingroles()
	{
		// Given
		var yuasa = await _tenrai.GetPersonVoiceActingRolesAsync(5068);

		// Then
		yuasa.Data.Should().BeEmpty();
	}

	[Fact]
	public async Task GetPersonVoiceActingRolesAsync_SekiTomokazuId_ShouldParseSekiTomokazuVoiceActingRoles()
	{
		// Given
		var seki = await _tenrai.GetPersonVoiceActingRolesAsync(1);

		// Then
		using (new AssertionScope())
		{
			seki.Data.Should().HaveCountGreaterThan(400);
			seki.Data.Should().Contain(x => x.Anime.Title.Equals("JoJo no Kimyou na Bouken Part 6: Stone Ocean") && x.Character.Name.Equals("Pucci, Enrico"));
			seki.Data.Should().Contain(x => x.Anime.Title.Equals("Fate/stay night: Unlimited Blade Works") && x.Character.Name.Equals("Gilgamesh"));
		}
	}

	[Fact]
	public async Task GetPersonVoiceActingRolesAsync_SugitaTomokazuId_ShouldParseSugitaTomokazuVoiceActingRoles()
	{
		// Given
		var sugita = await _tenrai.GetPersonVoiceActingRolesAsync(2);

		// Then
		using (new AssertionScope())
		{
			sugita.Data.Should().HaveCountGreaterThan(450);
			sugita.Data.Should().Contain(x => x.Anime.Title.Equals("JoJo no Kimyou na Bouken (TV)") && x.Character.Name.Equals("Joestar, Joseph"));
			sugita.Data.Should().Contain(x => x.Anime.Title.Equals("Gintama") && x.Character.Name.Equals("Sakata, Gintoki"));
		}
	}

	[Fact]
	public async Task GetPersonVoiceActingRolesAsync_Sfw_HasFewerRoles()
	{
		// Given
		const long voiceActorWithAdultRoles = 12243;

		// When
		var all = await _tenrai.GetPersonVoiceActingRolesAsync(voiceActorWithAdultRoles, new SfwConfig { Sfw = false });
		var sfw = await _tenrai.GetPersonVoiceActingRolesAsync(voiceActorWithAdultRoles, new SfwConfig { Sfw = true });

		// Then
		sfw.Data.Should().NotBeEmpty();
		sfw.Data.Count.Should().BeLessThan(all.Data.Count);
	}
}