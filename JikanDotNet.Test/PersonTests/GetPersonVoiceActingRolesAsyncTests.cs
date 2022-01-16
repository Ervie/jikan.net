using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
	public class GetPersonVoiceActingRolesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetPersonVoiceActingRolesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonVoiceActingRolesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonVoiceActingRolesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetPersonVoiceActingRolesAsync_YuasaId_ShouldParseMasaakiYuasaVoiceActingroles()
		{
			// Given
			var yuasa = await _jikan.GetPersonVoiceActingRolesAsync(5068);

			// Then
			yuasa.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task GetPersonVoiceActingRolesAsync_SekiTomokazuId_ShouldParseSekiTomokazuVoiceActingRoles()
		{
			// Given
			var seki = await _jikan.GetPersonVoiceActingRolesAsync(1);

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
			var sugita = await _jikan.GetPersonVoiceActingRolesAsync(2);

			// Then
			using (new AssertionScope())
			{
				sugita.Data.Should().HaveCountGreaterThan(450);
				sugita.Data.Should().Contain(x => x.Anime.Title.Equals("JoJo no Kimyou na Bouken (TV)") && x.Character.Name.Equals("Joestar, Joseph"));
				sugita.Data.Should().Contain(x => x.Anime.Title.Equals("Gintama") && x.Character.Name.Equals("Sakata, Gintoki"));
			}
		}
	}
}