using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.AnimeTests
{
    public class GetAnimeAsyncTests
    {
		private readonly IJikan _jikan;

		public GetAnimeAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<Anime>> func = _jikan.Awaiting(x => x.GetAnimeAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(5)]
		[InlineData(6)]
		public async Task GetAnimeAsync_CorrectId_ShouldReturnNotNullAnime(long malId)
        {
			// When
			var returnedAnime = await _jikan.GetAnimeAsync(malId);

			// Then
			returnedAnime.Should().NotBeNull();
        }

		[Theory]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		public async Task GetAnimeAsync_WrongId_ShouldThrowException(long malId)
		{
			// When
			Func<Task<Anime>> func = _jikan.Awaiting(x => x.GetAnimeAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetAnimeAsync_MSGundamId_ShouldParseGundam()
		{
			// When
			var gundamAnime = await _jikan.GetAnimeAsync(80);

			// Then
			gundamAnime.Title.Should().Be("Mobile Suit Gundam");
		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebop()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			bebopAnime.Title.Should().Be("Cowboy Bebop");
		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebopRelatedAnimeTypes()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebopAnime.Related.Should().HaveCount(2);
			}
		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebopRelatedAnimeSummaries()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			var relatedAnimeSummaries = bebopAnime.Related.Where(x => x.Relation.Equals("Summaries")).First();
			relatedAnimeSummaries.Items.Should().ContainSingle();

		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebopRelatedAnimeAdaptations()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			var relatedAnimeAdaptations = bebopAnime.Related.Where(x => x.Relation.Equals("Adaptations")).First();
			relatedAnimeAdaptations.Items.Should().ContainSingle();

		}

		[Fact]
		public async Task GetAnimeAsync_FSNId_ShouldParseFateStayNightRelatedAnime()
		{
			// When
			var fsnAnime = await _jikan.GetAnimeAsync(356);

			// Then
			var relatedAnimeAlternativeVersion = fsnAnime.Related.Where(x => x.Relation.Equals("Alternative Version")).First();
			relatedAnimeAlternativeVersion.Items.Should().ContainSingle();
		}

		[Fact]
		public async Task GetAnimeAsync_FSNReproductionId_ShouldParseFateStayNightReproductionRelatedAnime()
		{
			// When
			var fsnAnime = await _jikan.GetAnimeAsync(7559);

			// Then
			var relatedAnimeFullVersions = fsnAnime.Related.Where(x => x.Relation.Equals("Full Story")).First();
			using (new AssertionScope())
			{
				relatedAnimeFullVersions.Items.Should().ContainSingle();
				relatedAnimeFullVersions.Items.First().Name.Should().Be("Fate/stay night");
			}
		}

		[Fact]
		public async Task GetAnimeAsync_KamiNomiId_ShouldParseKamiNomiRelatedAnime()
		{
			// When
			var kamiNomi = await _jikan.GetAnimeAsync(17725);

			// Then
			var relatedAnimeParentStories = kamiNomi.Related.Where(x => x.Relation.Equals("Parent Story")).First();
			using (new AssertionScope())
			{
				relatedAnimeParentStories.Items.Should().ContainSingle();
				relatedAnimeParentStories.Items.First().Name.Should().Be("Kami nomi zo Shiru Sekai");
			}
		}

		[Fact]
		public async Task GetAnimeAsync_CardcaptorId_ShouldParseCardcaptorSakuraInformation()
		{
			// When
			var cardcaptor = await _jikan.GetAnimeAsync(232);

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
		public async Task GetAnimeAsync_AkiraId_ShouldParseAkiraCollections()
		{
			// When
			var akiraAnime = await _jikan.GetAnimeAsync(47);

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
		public async Task GetAnimeAsync_KeyTheMetalIdolId_ShouldParseAnimeWithNoRelatedAdaptations()
		{
			// Given
			var returnedAnime = await _jikan.GetAnimeAsync(1457);

			// When
			var relatedAnimeFirstAdaptation = returnedAnime.Related.Where(x => x.Relation.Equals("Adaptations")).FirstOrDefault();
			relatedAnimeFirstAdaptation.Should().BeNull();
		}
	}
}
