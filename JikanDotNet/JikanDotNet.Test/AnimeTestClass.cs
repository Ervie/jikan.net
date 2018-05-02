using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Test
{
    public class AnimeTestClass
    {
		private readonly IJikan jikan;

		public AnimeTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(5)]
		[InlineData(6)]
		public void ShouldReturnNotNullAnime(long malId)
        {
			Anime returnedAnime = Task.Run(() => jikan.GetAnime(malId)).Result;

			Assert.NotNull(returnedAnime);
        }

		[Theory]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		public void ShouldReturnNullAnime(long malId)
		{
			Anime returnedAnime = Task.Run(() => jikan.GetAnime(malId)).Result;

			Assert.Null(returnedAnime);
		}

		[Fact]
		public void ShouldParseGundam()
		{
			Anime gundamAnime = Task.Run(() => jikan.GetAnime(80)).Result;

			Assert.Equal("Mobile Suit Gundam", gundamAnime.Title);
		}

		[Fact]
		public void ShouldParseCowboyBebop()
		{
			Anime bebopAnime = Task.Run(() => jikan.GetAnime(1)).Result;

			Assert.Equal("Cowboy Bebop", bebopAnime.Title);
		}

		[Fact]
		public void ShouldParseCardcaptorSakuraInformation()
		{
			Anime cardcaptor = Task.Run(() => jikan.GetAnime(232)).Result;

			Assert.Equal("70", cardcaptor.Episodes);
			Assert.Equal("TV", cardcaptor.Type);
			Assert.Equal("Spring 1998", cardcaptor.Premiered);
			Assert.Equal("25 min. per ep.", cardcaptor.Duration);
			Assert.Equal("PG - Children", cardcaptor.Rating);
			Assert.Equal("Tuesdays at 18:00 (JST)", cardcaptor.Broadcast);
			Assert.Equal("Manga", cardcaptor.Source);
		}

		[Fact]
		public void ShouldParseAkiraCollections()
		{
			Anime akiraAnime = Task.Run(() => jikan.GetAnime(47)).Result;

			Assert.Equal(3, akiraAnime.Producers.Count);
			Assert.Equal(3, akiraAnime.Licensors.Count);
			Assert.Equal(1, akiraAnime.Studios.Count);
			Assert.Equal(6, akiraAnime.Genres.Count);
			Assert.Equal("Funimation", akiraAnime.Licensors.First().ToString());
			Assert.Equal("Tokyo Movie Shinsha", akiraAnime.Studios.First().ToString());
			Assert.Equal("Action", akiraAnime.Genres.First().ToString());
		}
	}
}
