using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	[Collection("JikanTests")]
	public class GetAnimeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeAsync(malId));

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
			var func = _jikan.Awaiting(x => x.GetAnimeAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetAnimeAsync_MSGundamId_ShouldParseGundam()
		{
			// When
			var gundamAnime = await _jikan.GetAnimeAsync(80);

			// Then
			gundamAnime.Data.Title.Should().Be("Kidou Senshi Gundam");
		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebop()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			bebopAnime.Data.Title.Should().Be("Cowboy Bebop");
		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebopImages()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebopAnime.Data.Images.JPG.ImageUrl.Should().NotBeNullOrEmpty();
				bebopAnime.Data.Images.JPG.SmallImageUrl.Should().NotBeNullOrEmpty();
				bebopAnime.Data.Images.JPG.LargeImageUrl.Should().NotBeNullOrEmpty();
				bebopAnime.Data.Images.WebP.ImageUrl.Should().NotBeNullOrEmpty();
				bebopAnime.Data.Images.WebP.SmallImageUrl.Should().NotBeNullOrEmpty();
				bebopAnime.Data.Images.WebP.LargeImageUrl.Should().NotBeNullOrEmpty();
			}
		}

		[Fact]
		public async Task GetAnimeAsync_BebopId_ShouldParseCowboyBebopTitles()
		{
			// When
			var bebopAnime = await _jikan.GetAnimeAsync(1);

			// Then
			using var _ = new AssertionScope();
			bebopAnime.Data.Titles.Should().HaveCount(3);
			bebopAnime.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Default") && x.Title.Equals("Cowboy Bebop"));
			bebopAnime.Data.Titles.Should().ContainSingle(x => x.Type.Equals("English") && x.Title.Equals("Cowboy Bebop"));
			bebopAnime.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Japanese") && x.Title.Equals("カウボーイビバップ"));
		}

		[Fact]
		public async Task GetAnimeAsync_CardcaptorId_ShouldParseCardcaptorSakuraInformation()
		{
			// When
			var cardcaptor = await _jikan.GetAnimeAsync(232);

			// Then
			using (new AssertionScope())
			{
				cardcaptor.Data.Episodes.Should().Be(70);
				cardcaptor.Data.Type.Should().Be("TV");
				cardcaptor.Data.Year.Should().Be(1998);
				cardcaptor.Data.Season.Should().Be(Season.Spring);
				cardcaptor.Data.Duration.Should().Be("25 min per ep");
				cardcaptor.Data.Rating.Should().Be("PG - Children");
				cardcaptor.Data.Broadcast.Day.Should().Be("Tuesdays");
				cardcaptor.Data.Broadcast.String.Should().Be("Tuesdays at 18:00 (JST)");
				cardcaptor.Data.Broadcast.Time.Should().Be("18:00");
				cardcaptor.Data.Broadcast.Timezone.Should().Be("Asia/Tokyo");
				cardcaptor.Data.Source.Should().Be("Manga");
				cardcaptor.Data.Approved.Should().BeTrue();
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
				akiraAnime.Data.Producers.Should().HaveCountGreaterOrEqualTo(1);
				akiraAnime.Data.Licensors.Should().HaveCount(1);
				akiraAnime.Data.Studios.Should().ContainSingle();
				akiraAnime.Data.Genres.Should().HaveCount(1);
				akiraAnime.Data.Approved.Should().BeTrue();
			}
		}
	}
}