using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
    public class AnimeTests
    {
		private readonly IJikan _jikan;

		public AnimeTests()
		{
			_jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(5)]
		[InlineData(6)]
		public async Task GetAnime_CorrectId_ShouldReturnNotNullAnime(long malId)
        {
			// When
			Anime returnedAnime = await _jikan.GetAnime(malId);

			// Then
			returnedAnime.Should().NotBeNull();
        }

		[Theory]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		public void GetAnime_WrongId_ShouldThrowException(long malId)
		{
			// When & Then
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.GetAnime(malId));
		}

		[Fact]
		public async Task GetAnime_MSGundamId_ShouldParseGundam()
		{
			// When
			Anime gundamAnime = await _jikan.GetAnime(80);

			// Then
			gundamAnime.Title.Should().Be("Mobile Suit Gundam");
		}

		[Fact]
		public async Task GetAnime_BebopId_ShouldParseCowboyBebop()
		{
			// When
			Anime bebopAnime = await _jikan.GetAnime(1);

			// Then
			bebopAnime.Title.Should().Be("Cowboy Bebop");
		}

		[Fact]
		public async Task GetAnime_BebopId_ShouldParseCowboyBebopRelatedAnime()
		{
			// When
			Anime bebopAnime = await _jikan.GetAnime(1);

			// Then
			using (new AssertionScope())
			{
				bebopAnime.Related.Adaptations.Should().HaveCount(2);
				bebopAnime.Related.Summaries.Should().ContainSingle();
			}
		}

		[Fact]
		public async Task GetAnime_FSNId_ShouldParseFateStayNightRelatedAnime()
		{
			// When
			Anime fsnAnime = await _jikan.GetAnime(356);

			// Then
			fsnAnime.Related.AlternativeVersions.Count.Should().BeGreaterThan(3);
		}

		[Fact]
		public async Task GetAnime_FSNReproductionId_ShouldParseFateStayNightReproductionRelatedAnime()
		{
			// When
			Anime fsnAnime = await _jikan.GetAnime(7559);

			// Then
			fsnAnime.Related.FullStories.First().Name.Should().Be("Fate/stay night");
		}

		[Fact]
		public async Task GetAnime_KamiNomiId_ShouldParseKamiNomiRelatedAnime()
		{
			// When
			Anime kamiNomi = await _jikan.GetAnime(17725);

			// Then
			kamiNomi.Related.ParentStories.First().Name.Should().Be("Kami nomi zo Shiru Sekai");
		}

		[Fact]
		public async Task GetAnime_CardcaptorId_ShouldParseCardcaptorSakuraInformation()
		{
			// When
			Anime cardcaptor = await _jikan.GetAnime(232);

			// Then
			using (new AssertionScope())
			{
				cardcaptor.Episodes.Should().Be(70);
				cardcaptor.Type.Should().Be("TV");
				cardcaptor.Premiered.Should().Be("Spring 1998");
				cardcaptor.Duration.Should().Be("25 min per ep");
				cardcaptor.Rating.Should().Be("PG - Children");
				cardcaptor.Broadcast.Should().Be("Tuesdays at 18:00 (JST)");
				cardcaptor.Source.Should().Be("Manga");
			}
		}

		[Fact]
		public async Task GetAnime_AkiraId_ShouldParseAkiraCollections()
		{
			// When
			Anime akiraAnime = await _jikan.GetAnime(47);

			// Then
			using (new AssertionScope())
			{
				akiraAnime.Producers.Should().HaveCount(3);
				akiraAnime.Licensors.Should().HaveCount(3);
				akiraAnime.Studios.Should().ContainSingle();
				akiraAnime.Genres.Should().HaveCount(7);
				akiraAnime.Licensors.First().ToString().Should().Be("Funimation");
				akiraAnime.Studios.First().ToString().Should().Be("Tokyo Movie Shinsha");
				akiraAnime.Genres.First().ToString().Should().Be("Action");
			}
		}

		[Fact]
		public async Task GetAnime_KeyTheMetalIdolId_ShouldParseAnimeWithNoRelatedAdaptations()
		{
			// Given
			Anime returnedAnime = await _jikan.GetAnime(1457);

			// When
			returnedAnime.Related.Adaptations.Should().BeNull();
		}
	}
}
